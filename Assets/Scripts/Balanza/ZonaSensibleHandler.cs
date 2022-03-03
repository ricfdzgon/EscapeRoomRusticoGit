using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaSensibleHandler : MonoBehaviour
{
    public delegate void ObjectEnterDelegate(SmartWeightProvider swp);
    public static event ObjectEnterDelegate OnObjectEnter;

    public delegate void ObjectExitDelegate(SmartWeightProvider swp);
    public static event ObjectExitDelegate OnObjectExit;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("ZonaSensibleHandler.OnTriggerEnter");
        if (other.gameObject.GetComponent<SmartWeightProvider>() != null)
        {
            if (OnObjectEnter != null)
            {
                OnObjectEnter(other.gameObject.GetComponent<SmartWeightProvider>());
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("ZonaSensibleHandler.OnTriggerExit");

        if (other.gameObject.GetComponent<SmartWeightProvider>() != null)
        {
            if (OnObjectExit != null)
            {
                OnObjectExit(other.gameObject.GetComponent<SmartWeightProvider>());
            }
        }

    }

}
