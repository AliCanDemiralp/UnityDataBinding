using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.UDB.Scripts.ListView
{
    public delegate void ItemSelected(Object selectedModel);


    public class ListView : MonoBehaviour
    {
        public event ItemSelected OnItemSelected;

        private Dictionary<Object, GameObject> _modelToView;

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
        private List<Object> _models;
        public List<Object> Models
        {
            set
            {
                _models = value;
                UpdateList();
            }
        }

        [SerializeField]
        private GameObject _defaltViewPrefab;

        void Awake()
        {
            _modelToView = new Dictionary<Object, GameObject>();
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

                _adapter = new TextListViewAdapter(_defaltViewPrefab);
            }

            UpdateList();
        }


        private void Update()
        {

        }

        public void FixedUpdate()
        {

        }



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

                //if the view doesn't have a button element attached, attach it 
                //and listen for presses. 
                Button b = listItem.GetComponent<Button>();
                if (b == null)
                    b = listItem.AddComponent<Button>();

                Object modelCapture = go;
                b.onClick.AddListener(() =>
                {
                    if (OnItemSelected != null)
                        OnItemSelected(modelCapture);
                });



                //TODO: ...
            }
        }
        #endregion
    }
}
