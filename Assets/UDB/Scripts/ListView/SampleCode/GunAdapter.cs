using UnityEngine;
using UnityEngine.UI;

namespace Assets.UDB.Scripts.ListView.SampleCode
{
    public class GunAdapter : IAdapter
    {
        public const string SlotDisplayChildName = "SlotDisplay";

        public GunListItem viewPrefab;
        
        
        public GameObject ModelToView(GameObject model)
        {
            //get number of slot consumed
            SlotConsumer scComponent = model.GetComponent<SlotConsumer>();

            //instantiate view prefab
            var view = (GameObject)GameObject.Instantiate(viewPrefab.gameObject);

            view.GetComponent<GunListItem>().SlotsTaken.text = scComponent.slotsConsumed.ToString();

            //TODO: ...

            return view;            
        }
    }
}
