using Block2D.Module.Blocks;
using Block2D.Module.Inputs;
using UnityEngine;
using UnityEngine.Events;

namespace Block2D.Module.Tiles
{
    public class TileSpawner : MonoBehaviour
    {
        public int row { get; private set; } = 9;

        public int column { get; private set; } = 6;

        public Tile[,] TileList { get; private set; }

        private float _gridSpace = 1.2f;
        public Vector3 gridOrigin = Vector3.zero;

        [SerializeField] private int _lastTileX;
        [SerializeField] private int _lastTileY;
        [SerializeField] private int _currentIndexTileX;
        [SerializeField] private int _currentIndexTileY;

        [SerializeField] private Tile gridPrefab;
        [SerializeField] private BlockSpawner _spawner;
        [SerializeField] private Sprite[] _blockSprites;

        private int maxX;
        private int maxY;

        private bool isFirst = true;

        private void Awake()
        {
            CreateGrid();
        }

        private void OnEnable()
        {
            Tile.OnTileHover += SetTileIndex;
            InputHandler.OnClick += AddScore;
            InputHandler.OnAttack += SwitchPattern;
        }

        private void OnDisable()
        {
            Tile.OnTileHover -= SetTileIndex;
            InputHandler.OnClick -= AddScore;
            InputHandler.OnAttack -= SwitchPattern;
        }
        private void Start()
        {
            _lastTileX = 0;
            _lastTileY = 0;

            maxX = row - 1;
            maxY = column - 1;
        }

        private void SetTileIndex(int x, int y)
        {
            _lastTileX = _currentIndexTileX;
            _lastTileY = _currentIndexTileY;

            _currentIndexTileX = x;
            _currentIndexTileY = y;

            SwitchPattern(false);
        }

        private void AddScore()
        {
            switch (_spawner.BlockIndex)
            {
                case 0:
                    GameManager.Instance.AddScore(2);
                    break;
                case 1:
                    GameManager.Instance.AddScore(1);
                    break;
                case 2:
                    GameManager.Instance.AddScore(2);
                    break;
                case 3:
                    GameManager.Instance.AddScore(1);
                    break;
            }
        }

        public void SwitchPattern(bool isAttack)
        {
            switch (_spawner.BlockIndex)
            {
                case 0:
                    if (isFirst)
                    {
                        RockPattern(isAttack);
                        isFirst = false;
                        return;
                    }
                    ResetRock();
                    RockPattern(isAttack);
                    break;
                case 1:
                    if (isFirst)
                    {
                        KnightPattern(isAttack);
                        isFirst = false;
                        return;
                    }
                    ResetKnight();
                    KnightPattern(isAttack);
                    break;
                case 2:
                    if (isFirst)
                    {
                        BishopPattern(isAttack);
                        isFirst = false;
                        return;
                    }
                    ResetBishop();
                    BishopPattern(isAttack);
                    break;
                case 3:
                    if (isFirst)
                    {
                        DragonPattern(isAttack);
                        isFirst = false;
                        return;
                    }
                    ResetDragon();
                    DragonPattern(isAttack);
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }

        private void CreateGrid()
        {
            TileList = new Tile[row, column];
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < column; y++)
                {
                    Vector3 spawnPosition = new Vector3(x * _gridSpace, y * _gridSpace, 0) + gridOrigin;
                    Tile gridObjects = Instantiate(gridPrefab, spawnPosition, Quaternion.identity, transform);

                    TileList[x, y] = gridObjects;

                    gridObjects.gameObject.name = "Tile( " + ("X:" + x + " ,Y:" + y + " )");
                    gridObjects.SetIndexTile(x, y);
                }
            }
        }

        private void RockPattern(bool isAttack)
        {
            TileList[_currentIndexTileX, _currentIndexTileY].ChangeSpriteBlock(_blockSprites[_spawner.BlockIndex]);

            var xIndex = _currentIndexTileX;
            var yIndex = _currentIndexTileY;

            for (int i = 0; i < 2; i++)
            {
                yIndex++;

                if (yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[_currentIndexTileX, yIndex].ChangeSpriteAttack(isAttack);
            }

            yIndex = _currentIndexTileY;
            for (int i = 0; i < 2; i++)
            {
                yIndex--;

                if (yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[_currentIndexTileX, yIndex].ChangeSpriteAttack(isAttack);
            }

            for (int i = 0; i < 2; i++)
            {
                xIndex++;

                if (xIndex < 0 || xIndex > maxX)
                {
                    break;
                }

                TileList[xIndex, _currentIndexTileY].ChangeSpriteAttack(isAttack);
            }

            xIndex = _currentIndexTileX;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;

                if (xIndex < 0 || xIndex > maxX)
                {
                    break;
                }

                TileList[xIndex, _currentIndexTileY].ChangeSpriteAttack(isAttack);
            }
        }

        private void ResetRock()
        {
            var xIndex = _lastTileX;
            var yIndex = _lastTileY;

            if (!TileList[xIndex, yIndex].isOccupied)
            {
                TileList[xIndex, yIndex].ResetBool();
            }

            for (int i = 0; i < 2; i++)
            {
                yIndex++;

                if (yIndex < 0 || yIndex > maxY)
                {
                    break;
                }

                TileList[_lastTileX, yIndex].ResetSprite();

            }

            yIndex = _lastTileY;
            for (int i = 0; i < 2; i++)
            {
                yIndex--;

                if (yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[_lastTileX, yIndex].ResetSprite();
            }

            for (int i = 0; i < 2; i++)
            {
                xIndex++;

                if (xIndex < 0 || xIndex > maxX)
                {
                    break;
                }

                TileList[xIndex, _lastTileY].ResetSprite();
            }

            xIndex = _lastTileX;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;

                if (xIndex < 0 || xIndex > maxX)
                {
                    break;
                }

                TileList[xIndex, _lastTileY].ResetSprite();
            }
        }

        private void BishopPattern(bool isAttack)
        {
            TileList[_currentIndexTileX, _currentIndexTileY].ChangeSpriteBlock(_blockSprites[_spawner.BlockIndex]);

            var xIndex = _currentIndexTileX;
            var yIndex = _currentIndexTileY;

            for (int i = 0; i < 2; i++)
            {
                xIndex++;
                yIndex++;

                if (xIndex < 0 || xIndex > maxX || yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[xIndex, yIndex].ChangeSpriteAttack(isAttack);
            }

            xIndex = _currentIndexTileX;
            yIndex = _currentIndexTileY;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                if (xIndex < 0 || xIndex > maxX || yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[xIndex, yIndex].ChangeSpriteAttack(isAttack);
            }

            xIndex = _currentIndexTileX;
            yIndex = _currentIndexTileY;
            for (int i = 0; i < 2; i++)
            {
                xIndex++;
                yIndex--;

                if (xIndex < 0 || xIndex > maxX || yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[xIndex, yIndex].ChangeSpriteAttack(isAttack);
            }

            xIndex = _currentIndexTileX;
            yIndex = _currentIndexTileY;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex--;

                if (xIndex < 0 || xIndex > maxX || yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[xIndex, yIndex].ChangeSpriteAttack(isAttack);
            }
        }

        private void ResetBishop()
        {
            var xIndex = _lastTileX;
            var yIndex = _lastTileY;

            if (!TileList[xIndex, yIndex].isOccupied)
            {
                TileList[xIndex, yIndex].ResetBool();
            }

            for (int i = 0; i < 2; i++)
            {
                xIndex++;
                yIndex++;

                if (xIndex < 0 || xIndex > maxX || yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[xIndex, yIndex].ResetSprite();
            }

            xIndex = _lastTileX;
            yIndex = _lastTileY;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                if (xIndex < 0 || xIndex > maxX || yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[xIndex, yIndex].ResetSprite();
            }

            xIndex = _lastTileX;
            yIndex = _lastTileY;
            for (int i = 0; i < 2; i++)
            {
                xIndex++;
                yIndex--;

                if (xIndex < 0 || xIndex > maxX || yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[xIndex, yIndex].ResetSprite();
            }

            xIndex = _lastTileX;
            yIndex = _lastTileY;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex--;

                if (xIndex < 0 || xIndex > maxX || yIndex < 0 || yIndex > maxY)
                {
                    break;
                }
                TileList[xIndex, yIndex].ResetSprite();
            }
        }

        private void KnightPattern(bool isAttack)
        {
            TileList[_currentIndexTileX, _currentIndexTileY].ChangeSpriteBlock(_blockSprites[_spawner.BlockIndex]);

            //atas kanan
            var xIndex = 3;
            var yIndex = 0;

            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                var tempX = _currentIndexTileX + xIndex;
                var tempY = _currentIndexTileY + yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ChangeSpriteAttack(isAttack);
            }

            //atas kiri
            xIndex = 3;
            yIndex = 0;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                var tempX = _currentIndexTileX - xIndex;
                var tempY = _currentIndexTileY + yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ChangeSpriteAttack(isAttack);
            }

            //bawah kanan
            xIndex = 3;
            yIndex = 0;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                var tempX = _currentIndexTileX + xIndex;
                var tempY = _currentIndexTileY - yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ChangeSpriteAttack(isAttack);
            }

            //bawah kiri
            xIndex = 3;
            yIndex = 0;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                var tempX = _currentIndexTileX - xIndex;
                var tempY = _currentIndexTileY - yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ChangeSpriteAttack(isAttack);
            }
        }

        private void ResetKnight()
        {
            if (!TileList[_lastTileX, _lastTileY].isOccupied)
            {
                TileList[_lastTileX, _lastTileY].ResetBool();
            }

            //atas kanan
            var xIndex = 3;
            var yIndex = 0;

            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                var tempX = _lastTileX + xIndex;
                var tempY = _lastTileY + yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ResetSprite();
            }

            //atas kiri
            xIndex = 3;
            yIndex = 0;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                var tempX = _lastTileX - xIndex;
                var tempY = _lastTileY + yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ResetSprite();
            }

            //bawah kanan
            xIndex = 3;
            yIndex = 0;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                var tempX = _lastTileX + xIndex;
                var tempY = _lastTileY - yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ResetSprite();
            }

            //bawah kiri
            xIndex = 3;
            yIndex = 0;
            for (int i = 0; i < 2; i++)
            {
                xIndex--;
                yIndex++;

                var tempX = _lastTileX - xIndex;
                var tempY = _lastTileY - yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ResetSprite();
            }
        }

        private void DragonPattern(bool isAttack)
        {
            TileList[_currentIndexTileX, _currentIndexTileY].ChangeSpriteBlock(_blockSprites[_spawner.BlockIndex]);

            //atas kanan
            var xIndex = -1;
            var yIndex = 1;

            for (int i = 0; i < 2; i++)
            {
                xIndex++;

                var tempX = _currentIndexTileX + xIndex;
                var tempY = _currentIndexTileY + yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ChangeSpriteAttack(isAttack);
            }

            //atas kiri
            xIndex = 1;
            yIndex = -1;
            for (int i = 0; i < 2; i++)
            {
                yIndex++;

                var tempX = _currentIndexTileX + xIndex;
                var tempY = _currentIndexTileY - yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ChangeSpriteAttack(isAttack);
            }

            //bawah kanan
            xIndex = -1;
            yIndex = 1;
            for (int i = 0; i < 2; i++)
            {
                xIndex++;

                var tempX = _currentIndexTileX - xIndex;
                var tempY = _currentIndexTileY - yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ChangeSpriteAttack(isAttack);
            }

            //bawah kiri
            xIndex = 1;
            yIndex = -1;
            for (int i = 0; i < 2; i++)
            {
                yIndex++;

                var tempX = _currentIndexTileX - xIndex;
                var tempY = _currentIndexTileY + yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ChangeSpriteAttack(isAttack);
            }
        }

        private void ResetDragon()
        {
            if (!TileList[_lastTileX, _lastTileY].isOccupied)
            {
                TileList[_lastTileX, _lastTileY].ResetBool();
            }

            //atas kanan
            var xIndex = -1;
            var yIndex = 1;

            for (int i = 0; i < 2; i++)
            {
                xIndex++;

                var tempX = _lastTileX + xIndex;
                var tempY = _lastTileY + yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ResetSprite();
            }

            //atas kiri
            xIndex = 1;
            yIndex = -1;
            for (int i = 0; i < 2; i++)
            {
                yIndex++;

                var tempX = _lastTileX + xIndex;
                var tempY = _lastTileY - yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ResetSprite();
            }

            //bawah kanan
            xIndex = -1;
            yIndex = 1;
            for (int i = 0; i < 2; i++)
            {
                xIndex++;

                var tempX = _lastTileX - xIndex;
                var tempY = _lastTileY - yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ResetSprite();
            }

            //bawah kiri
            xIndex = 1;
            yIndex = -1;
            for (int i = 0; i < 2; i++)
            {
                yIndex++;

                var tempX = _lastTileX - xIndex;
                var tempY = _lastTileY + yIndex;

                if (tempX < 0 || tempX > maxX || tempY < 0 || tempY > maxY)
                {
                    break;
                }
                TileList[tempX, tempY].ResetSprite();
            }
        }
    }
}