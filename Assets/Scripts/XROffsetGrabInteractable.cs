using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Dubidaba se este script xa o aplicaramos ás rodas
//xa vexo que si
//Lembrade que gracias ó OFFSET conseguimos que o mando
//non xire no momento que agarramos
//O seguinte que imos a facer é crear o script que nos servirá
//para que a roda se quede en certas posicións ó soltala
// A ese script chamarémoslle SnapTurnDial
public class XROffsetGrabInteractable : XRGrabInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        if(attachTransform == null) {
            GameObject attach = new GameObject("Attach");
            attach.transform.SetParent(gameObject.transform);
            attachTransform = attach.transform;
        }
    }

    // Update is called once per frame
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
