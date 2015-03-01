using UnityEngine;
using UnityEngine.UI;
namespace Assets.UDB.Scripts.ListView
{
    public class VerticalLayoutViewStrategy : IListViewLayoutStrategy
    {
        private VerticalLayoutGroup _verticalLayout;
        private MonoBehaviour _mbContext;



        public VerticalLayoutViewStrategy(MonoBehaviour mbContext)
        {
            _mbContext = mbContext;
            _verticalLayout = _mbContext.GetComponentInChildren<VerticalLayoutGroup>();

            if (_verticalLayout == null)
                throw new LayoutException("VerticalLayoutViewStrategy requires a VerticalLayoutGroup as a child of the GameObject with ListView component.");
        }


        public void SetItemTransform(GameObject item, ListView listView)
        {
            item.transform.parent = _verticalLayout.transform;
        }
    }
}
