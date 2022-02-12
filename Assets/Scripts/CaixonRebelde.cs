using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CaixonRebelde : MonoBehaviour
{
    public ConfigurableJoint configurableJoint;
    // Start is called before the first frame update
    void Start()
    {
        configurableJoint.zMotion = ConfigurableJointMotion.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Liberar(bool liberar) {
        if(liberar) {
            configurableJoint.zMotion = ConfigurableJointMotion.Limited;
        } else {
            configurableJoint.zMotion = ConfigurableJointMotion.Locked;
        }
    }
}
