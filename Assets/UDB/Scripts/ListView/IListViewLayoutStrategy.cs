using UnityEngine;

namespace Assets.UDB.Scripts.ListView
{
    public interface IListViewLayoutStrategy
    {
        void SetItemTransform(GameObject item, ListView listView);        
    }
}
