using System.Collections.Generic;
using UnityEngine;

namespace Assets.UDB.ListView
{
    public class ListView : MonoBehaviour
    {
        #region Editor
        public List<GameObject> models;
        #endregion




        private Dictionary<GameObject, GameObject> _modelToView;


        private IAdapter _adapter;
        public IAdapter Adapter
        {
            set
            {
                _adapter = value;
                UpdateList();
            }
        }


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

            if (models == null)
                models = new List<GameObject>();

            UpdateList();
        }


        void Update()
        {

        }

        #region Private
        private void UpdateList()
        {
            _modelToView.Clear();


            //Re-generate model to view.
            foreach (var go in _models)
            {
                //Inflate the view for the model using adapter;
                GameObject listItem = _adapter.ModelToView(go);

                //TODO: ...
            }
        }
        #endregion

    }
}
