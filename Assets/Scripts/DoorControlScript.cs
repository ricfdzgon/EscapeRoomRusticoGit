using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControlScript : MonoBehaviour
{

    public GameObject[] colorDials;
    private HingeJoint hinge;
    void Start()
    {
        //Nos tenemos que suscribir a cada uno de nuestros diales
        for (int i = 0; i < colorDials.Length; i++)
        {
            colorDials[i].GetComponentInChildren<SnapTurnDial>().OnCodeChanged += codeHasChangeOuIsoDin;
        }
        hinge = GetComponent<HingeJoint>();
    }

    void OnDestroy()
    {
        //Debemos anular a suscripcion o evento OnCodeChanged cando se destrue a porta
        for (int i = 0; i < colorDials.Length; i++)
        {
            colorDials[i].GetComponentInChildren<SnapTurnDial>().OnCodeChanged -= codeHasChangeOuIsoDin;
        }
    }

    void Update()
    {

    }

    public void codeHasChangeOuIsoDin()
    {
        if (CodeCheck())
        {
            Open();
        }
    }

    private bool CodeCheck()
    {
        for (int i = 0; i < colorDials.Length; i++)
        {
            if (colorDials[i].GetComponentInChildren<SnapTurnDial>() == null)
            {
                Debug.Log("DoorControlScript.Open null SpanTurnDial " + i);
                return false;
            }
            if (colorDials[i].GetComponentInChildren<SnapTurnDial>().CodeCheck() == false)
            {
                return false;
            }
        }
        return true;

    }

    private void Open()
    {
        //  Debug.Log("DoorControlScript.Open A abriríase se souperamos como");
        var motor = hinge.motor;
        motor.force = 100;
        motor.targetVelocity = 90;
        motor.freeSpin = false;
        hinge.motor = motor;
        hinge.useMotor = true;
    }
}
