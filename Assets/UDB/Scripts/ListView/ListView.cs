using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UDB.Scripts.ListView
{
    public class ListView : MonoBehaviour
    {
        private Dictionary<GameObject, GameObject> _modelToView;


        private IListViewLayoutStrategy _layoutStrategy;
        public IListViewLayoutStrategy LayoutStrategy
        {
            set
            {
                _layoutStrategy = value;
                UpdateLayout();
            }
        }


        private IAdapter _adapter;
        public IAdapter Adapter
        {
            set
            {
                _adapter = value;
                UpdateList();
            }
        }


        [SerializeField]
        private ICollection<GameObject> _models;
        public ICollection<GameObject> Models
        {
            set
            {
                _models = value;
                UpdateList();
            }
        }


        void Start()
        {
            _modelToView = new Dictionary<GameObject, GameObject>();
            UpdateList();
        }


        void Update()
        {

        }

        /// <summary>
        /// TODO: Implement
        /// </summary>
        #region Private
        private void UpdateLayout()
        {
            //TODO: Implement
        }

        private void UpdateList()
        {
            _modelToView.Clear();


            //Re-generate model to view.
            foreach (var go in _models)
            {
                
                //Inflate the view for the model using adapter;
                GameObject listItem = _adapter.ModelToView(go);

                //cache
                _modelToView[go] = listItem;

                //TODO: ...

            }
        }
        #endregion
    }
}
