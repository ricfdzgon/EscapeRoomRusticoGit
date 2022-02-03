using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    void Start()
    {
        if (attachTransform == null)
        {
            GameObject attach = new GameObject("Attach");
            attach.transform.SetParent(gameObject.transform);
            attachTransform = attach.transform;
        }
    }

    void Update()
    {

    }

    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        attachTransform.position = interactor.transform.position;
        attachTransform.rotation = interactor.transform.rotation;


        base.OnSelectEntering(interactor);
    }
}
