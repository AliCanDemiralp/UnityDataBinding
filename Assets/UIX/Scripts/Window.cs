using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.UIX.Scripts
{
    public class Window : MonoBehaviour 
    {
        private RectTransform _targetTransform;

        private void Start()
        {
            _targetTransform = GetComponent<RectTransform>();
        }
        public void OnDrag(PointerEventData eventData)
        {
            _targetTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
        }
    }
}
