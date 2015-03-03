using System;
using System.Collections.Generic;
using Assets.UDB.Scripts.Core.Extensions;
using Mono.CSharp;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UIX.Scripts
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

        private void OnEnable()
        {
            var scrollRect = transform.FindChild("Scroll Rectangle").GetComponent<ScrollRect>();
            scrollRect.verticalNormalizedPosition   = 0;
            scrollRect.horizontalNormalizedPosition = 0;
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
