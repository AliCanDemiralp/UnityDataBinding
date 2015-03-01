using Assets.UDB.Scripts.ListView;
using Assets.UDB.Scripts.ListView.SampleCode;
using UnityEngine;
using System.Collections;

public class GunConstructionController : MonoBehaviour
{
    public GunListItem viewPrefab;

    // Use this for initialization
    void Start()
    {
        var ga = new GunAdapter();
        ga.viewPrefab = viewPrefab;

        GetComponent<ListView>().Adapter = ga;   
    }

    // Update is called once per frame
    void Update()
    {

    }
}
