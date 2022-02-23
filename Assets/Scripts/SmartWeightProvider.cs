using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartWeightProvider : MonoBehaviour
{
    public delegate void GravitingChangedDelegate();
    public event GravitingChangedDelegate OnGravitingChanged;

    public float weigth;
    private bool isGraviting;
    // Start is called before the first frame update
    void Start()
    {
        isGraviting = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getWeigth()
    {
        if (isGraviting)
        {
            return weigth;
        }
        return 0;
    }

    public void SetGraviting(bool graviting)
    {
        isGraviting = graviting;
        if (OnGravitingChanged != null)
        {
            OnGravitingChanged();
        }
    }
}
