using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPuzleGrabInteractable : XRGrabInteractable
{
    private PuzllePieceIdentifier ppi;
    void Start()
    {
        ppi = GetComponent<PuzllePieceIdentifier>();
    }

    void Update()
    {

    }

    public override bool IsHoverableBy(XRBaseInteractor interactor)
    {
        if (interactor is XRSocketInteractor)
        {
            PuzllePieceIdentifier ppiSocket = interactor.gameObject.GetComponentInParent<PuzllePieceIdentifier>();
            if (ppiSocket != null)
            {
                if (ppi.fila == ppiSocket.fila && ppi.columna - ppiSocket.columna == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return base.IsHoverableBy(interactor);
    }
}
