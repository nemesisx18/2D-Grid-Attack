using Block2D.Module.Tiles;
using System.Collections;
using UnityEngine;

namespace Block2D.Module.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private bool _isOverlap;
        
        private Ray _ray;
        private RaycastHit2D _hit;

        private Camera _cam;

        public delegate void OnInputMouse();
        public delegate void OnInputAttack(bool isAttack);
        public static event OnInputMouse OnClick;
        public static event OnInputAttack OnAttack;

        private void Start()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (!GameManager.Instance.isGameOver)
            {
                OnHover(_ray);
            }
        }

        private void OnHover(Ray ray)
        {
            _hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (_hit.collider == null)
            {
                return;
            }
            else
            {
                IRaycastable raycastableObj = _hit.collider.GetComponent<IRaycastable>();

                if(Input.GetMouseButtonDown(0))
                {
                    raycastableObj?.OnMouseClick();
                    StartCoroutine(DelaySwitch());
                    OnAttack?.Invoke(true);
                    return;
                }

                raycastableObj?.OnMouseHover();
            }
        }

        private IEnumerator DelaySwitch()
        {
            yield return new WaitForSeconds(0.5f);
            OnClick?.Invoke();
        }
    }
}