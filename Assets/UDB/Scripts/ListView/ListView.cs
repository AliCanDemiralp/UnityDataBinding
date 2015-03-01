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
        private List<GameObject> _models;
        public List<GameObject> Models
        {
            set
            {
                _models = value;
                UpdateList();
            }
        }

        [SerializeField]
        private GameObject _defaultListItem;

        void Awake()
        {
            _modelToView = new Dictionary<GameObject, GameObject>();
            _layoutStrategy = new VerticalLayoutViewStrategy(this);           
        }

        void Start()
        {
            if (_adapter == null)
            {
                Debug.LogWarning("ListView does not have an adapter. Setting to default."
                + " Please implement IAdapter and set Adapter property of ListView."
                + " Make sure that the MonoBehavior that sets the IAdapter is earlier than ListView"
                + " in Edit->Project Settings->Script Execution Order.");

                _adapter = new TextListViewAdapter(_defaultListItem, this);
            }

            UpdateList();
        }


        private void Update()
        {

        }

        public void FixedUpdate()
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
            //clear the list
            foreach (GameObject view in _modelToView.Values)
                Destroy(view);

            _modelToView.Clear();

            //Re-generate model to view.
            foreach (var go in _models)
            {

                //Inflate the view for the model using adapter;
                GameObject listItem = _adapter.ModelToView(go);

                _layoutStrategy.SetItemTransform(listItem, this);

                //cache
                _modelToView[go] = listItem;

                //TODO: ...
            }
        }
        #endregion
    }
}
