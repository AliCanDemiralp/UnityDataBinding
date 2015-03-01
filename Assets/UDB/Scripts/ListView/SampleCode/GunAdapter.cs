using UnityEngine;

namespace Assets.UDB.Scripts.ListView.SampleCode
{
    public class GunAdapter : MonoBehaviour, IAdapter
    {
        public const string SlotDisplayChildName = "SlotDisplay";

        public GameObject viewPrefab;
        


        public GameObject ModelToView(GameObject model)
        {
            //get number of slot consumed
            SlotConsumer scComponent = model.GetComponent<SlotConsumer>();

            //instantiate view prefab
            GameObject view = (GameObject)Instantiate(viewPrefab);

            //TODO: ...

            return view;            
        }
    }
}
