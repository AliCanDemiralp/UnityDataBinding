﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.UDB.Scripts.ListView
{
    /// <summary>
    /// Pretty much an "error" adapter in most cases. Only displays the name of the model game object. 
    /// </summary>
    public class TextListViewAdapter : IAdapter
    {        
        private GameObject _viewPrefab;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mbContext">A mono behaviour that this adapter acts on behalf of.</param>
        public TextListViewAdapter(GameObject viewPrefab)
        {            
            _viewPrefab = viewPrefab;
        }


        public GameObject ModelToView(Object model)
        {
            GameObject view = GameObject.Instantiate(_viewPrefab) as GameObject;

            Text text = view.GetComponentInChildren<Text>();
            text.text = model.name;                        

            return view;
        }
    }
}
