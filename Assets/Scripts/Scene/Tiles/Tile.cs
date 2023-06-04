using UnityEngine;

namespace Block2D.Module.Tiles
{
    public class Tile : MonoBehaviour, IRaycastable
    {
        [SerializeField] private Sprite _blockSprite;
        [SerializeField] private Sprite _attackSprite;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private SpriteRenderer _tileRenderer;

        [SerializeField] private bool _isClickable = false;
        
        public int TileIndexX { get; private set; }
        public int TileIndexY { get; private set; }

        public bool isOccupied = false;

        public delegate void OnTileSelected(int x, int y);
        public static event OnTileSelected OnTileHover;

        public void SetIndexTile(int tileIndexX, int tileIndexY)
        {
            TileIndexX = tileIndexX;
            TileIndexY = tileIndexY;
        }

        public void OnMouseHover()
        {
            if (!_isClickable)
            {
                OnTileHover?.Invoke(TileIndexX, TileIndexY);
                _isClickable = true;
            }
        }

        public void ChangeSpriteAttack(bool isAttack)
        {
            if (isAttack)
            {
                if (isOccupied)
                {
                    GameManager.Instance.GameOver();
                    return;
                }
            }
            else
            {
                if (!isOccupied)
                {
                    _tileRenderer.sprite = _attackSprite;
                }
            }
        }

        public void ChangeSpriteBlock(Sprite sprite)
        {
            _blockSprite = sprite;
            _tileRenderer.sprite = sprite;
        }

        public void ResetSprite()
        {
            if (!isOccupied)
            {
                _tileRenderer.sprite = _defaultSprite;
            }
        }

        public void ResetBool()
        {
            _isClickable = false;
            _tileRenderer.sprite = _defaultSprite;
        }

        public void OnMouseClick()
        {
            _tileRenderer.sprite = _blockSprite;
            isOccupied = true;
            _isClickable = true;
        }
    }
}