using UnityEngine;

namespace Block2D.Module.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private int _score;

        public Sprite BlockSprite { get; private set; }

        public int Score => _score;

        private void Start()
        {
            BlockSprite = GetComponent<SpriteRenderer>().sprite;
        }
    }
}
