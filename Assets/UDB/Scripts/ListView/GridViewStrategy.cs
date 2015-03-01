using UnityEngine;
using UnityEngine.UI;
namespace Assets.UDB.Scripts.ListView
{
    public class GridViewStrategy : IListViewLayoutStrategy
    {
        private GridLayoutGroup _gridLayout;
        private MonoBehaviour _mbContext;



        public GridViewStrategy(MonoBehaviour mbContext)
        {
            _mbContext = mbContext;
            _gridLayout = _mbContext.GetComponentInChildren<GridLayoutGroup>();

            if (_gridLayout == null)
                throw new LayoutException("GridViewStrategy requires a GridLayoutGroup as a child of the GameObject with ListView component.");
        }


        public void SetItemTransform(GameObject item, ListView listView)
        {
            //TODO: Implement
            throw new System.NotImplementedException();
        }
    }
}
