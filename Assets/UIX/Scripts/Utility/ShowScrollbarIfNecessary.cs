namespace UnityEngine.UI.Extensions
{
    public class ShowScrollbarIfNecessary : MonoBehaviour 
    {
        public ScrollRect       ScrollRectangle;
        public RectTransform    Contents;

        private void Update ()
        {
            var verticalScrollbar   = ScrollRectangle.verticalScrollbar;
            var horizontalScrollbar = ScrollRectangle.horizontalScrollbar;
            var scrollRectTransform = (RectTransform) ScrollRectangle.transform;

            if(verticalScrollbar)
                verticalScrollbar   .gameObject.SetActive(scrollRectTransform.rect.height   < Contents.rect.height);
            if(horizontalScrollbar)
                horizontalScrollbar .gameObject.SetActive(scrollRectTransform.rect.width    < Contents.rect.width);
        }
    }
}
