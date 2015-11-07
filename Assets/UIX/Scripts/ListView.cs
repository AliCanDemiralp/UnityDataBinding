using System;
using System.Collections.Generic;
using UnityEngine.DataBinding.Extensions;
using Mono.CSharp;

namespace UnityEngine.UI.Extensions
{
    public class ListView : MonoBehaviour
    {
        public GameObject                   ListItem;

        public IEnumerable<object>          Data;
        public Action<object, GameObject>   AdapterMethod;

        [SerializeField]
        private string _adapterString;
        public  string AdapterString
        {
            get { return _adapterString; }
            set
            {
                _adapterString = value;
                Debug.Log("Adapter string updated!");
                UpdateAdapterMethodFromString();
            }
        }

        private readonly Dictionary<object, GameObject> _cache 
                   = new Dictionary<object, GameObject>();

        private void Start()
        {
            // Adjust grid size to list item boundary.
            if (ListItem != null)
            {
                var gridLayout          = GetComponentInChildren<GridLayoutGroup>();
                var listItemTransform   = (RectTransform) ListItem.transform;
                // TODO - Get boundary of list item and set grid cell size to it.
            }
        }
        private void Update()
        {
            if (AdapterMethod == null && !string.IsNullOrEmpty(AdapterString))
            {
                UpdateAdapterMethodFromString();
                return;
            }
        
            foreach (var datum in Data)
            {
                GameObject item;

                if (_cache.ContainsKey(datum))
                    item = _cache[datum];
                else
                {
                    item = Instantiate(ListItem) as GameObject;
                    _cache[datum] = item;
                }

                if(AdapterMethod != null)
                    AdapterMethod.Invoke(datum, item);

                item.transform.SetParent(GetComponentInChildren<GridLayoutGroup>().transform, false);
            }

            // Remove unused cache items.
        }

        private void UpdateAdapterMethodFromString()
        {
            EvaluatorExtensions.Prepare();
            try
            {
                AdapterMethod = (Action<object, GameObject>)Evaluator.Evaluate(
                    "new Action<object, GameObject>((data, view) => { " + _adapterString + " });");
            }
            catch (Exception e)
            {
                Debug.Log("Syntax error on adapter string of " + ToString() + " : " + _adapterString + " - " + e.Message);
                return;
            }
            Debug.Log("Adapter method set from string: " + _adapterString);
        }
    }
}
