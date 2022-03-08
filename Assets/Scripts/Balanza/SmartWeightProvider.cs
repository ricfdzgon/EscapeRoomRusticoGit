using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartWeightProvider : MonoBehaviour
{
    public delegate void GravitingChangedDelegate();
    public event GravitingChangedDelegate OnGravitingChanged;
    private Rigidbody rb;
    public float weigth;
    private bool isGraviting;
    // Start is called before the first frame update
    void Start()
    {
        isGraviting = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getWeigth()
    {
        if (isGraviting)
        {
            if (rb.velocity.magnitude < 0.01)
            {
                return weigth;
            }
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

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("SnapWeightProvider.OnCollisionEnter: Ha pasado por aquí");
        if (OnGravitingChanged != null)
        {
            OnGravitingChanged();
        }
    }
}
