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
                return isSocketCompatible(ppi, ppiSocket, interactor.gameObject.tag);
            }
        }
        return base.IsHoverableBy(interactor);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        if (interactor is XRSocketInteractor)
        {
            PuzllePieceIdentifier ppiSocket = interactor.gameObject.GetComponentInParent<PuzllePieceIdentifier>();
            if (ppiSocket != null)
            {
                return isSocketCompatible(ppi, ppiSocket, interactor.gameObject.tag);
            }
        }
        return base.IsSelectableBy(interactor);
    }

    private bool isSocketCompatible(PuzllePieceIdentifier ppiPropio, PuzllePieceIdentifier ppiSocket, string socketTag)
    {
        if (socketTag == "SocketE")
        {
            if (ppiPropio.fila == ppiSocket.fila && ppiPropio.columna - ppiSocket.columna == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (ppiPropio.columna == ppiSocket.columna && ppiPropio.fila - ppiSocket.fila == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
