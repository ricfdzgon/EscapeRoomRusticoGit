using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartWeightProvider : MonoBehaviour
{
    public delegate void GravitingChangedDelegate();
    public event GravitingChangedDelegate OnGravitingChanged;
    private Rigidbody rb;
    public float weigth;
    public Transform rayCastOrigin;
    private bool isGraviting;
    private float rayCastDistance = 0.02f;
    private bool previousHit = false;
    // Start is called before the first frame update
    void Start()
    {
        isGraviting = true;
        rb = GetComponent<Rigidbody>();

        if (rayCastOrigin == null)
        {
            Debug.Log("RaycastOrigin no establecido");
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool hit = Physics.Raycast(rayCastOrigin.position, transform.TransformDirection(Vector3.down), rayCastDistance);
        if (hit != previousHit)
        {
            previousHit = hit;
            if (OnGravitingChanged != null)
            {
                OnGravitingChanged();
            }
        }
    }

    public float getWeigth()
    {
        if (isGraviting)
        {
            /*
            if (rb.velocity.magnitude < 0.01)
            {
                return weigth;
            }*/
            if (previousHit)
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
