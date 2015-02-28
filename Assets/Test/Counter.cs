using UnityEngine;

public class Counter : MonoBehaviour 
{
    public int Count { get; private set; }

    public int[] List = {1, 2, 3};

    public void Increment()
    {
        Count ++;
    }
}
