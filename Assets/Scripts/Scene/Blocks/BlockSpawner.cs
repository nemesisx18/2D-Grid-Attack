using Block2D.Module.Inputs;
using UnityEngine;

namespace Block2D.Module.Blocks
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private Sprite[] _blockPrefabs;

        private Sprite _nextBlockSprite;

        public int BlockIndex { get; private set; }

        public delegate void OnNextBlockAvaiable(Sprite sprite);
        public static event OnNextBlockAvaiable OnChangeImage;

        private void OnEnable()
        {
            InputHandler.OnClick += InitRandomBlock;
        }

        private void OnDisable()
        {
            InputHandler.OnClick -= InitRandomBlock;
        }

        private void Start()
        {
            InitRandomBlock();
        }

        [ContextMenu("Init Block")]
        private void InitRandomBlock()
        {
            int blockIndex = Random.Range(0, _blockPrefabs.Length);

            _nextBlockSprite = _blockPrefabs[blockIndex];
            BlockIndex = blockIndex;
            OnChangeImage?.Invoke(_nextBlockSprite);
        }
    }
}