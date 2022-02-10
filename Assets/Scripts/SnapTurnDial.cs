using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapTurnDial : MonoBehaviour
{
    public int correctIndex;
    // Necesitamos una lista de los ángulos nos que a roda se debe deter
    //Creanika publica para poder establecer estes angulos no inspector de Unity
    public float[] snapAngles;
    // En otra variable establecemos cal e o xeito no que xira a roda
    // esa e a maneria de que podamos saber cal e o angulo de Euler que temos
    // que regular dende este script
    public int turnAxis;
    public XRBaseControllerInteractor baseController;

    private float hapticFeedBackRange;
    private bool hapticFeedBackControl;

    // Esta variable permite a roda xirar libremente mentres está agarrada
    // e que se "pegue" a un dos ángulos pre establecidos cando se solta
    private bool rotacionLibre;

    void Start()
    {
        rotacionLibre = false;
        hapticFeedBackRange = 2.0f;
        hapticFeedBackControl = true;
    }

    void Update()
    {
        // En cada frame comprobamos se a rotacion esta liberada
        // se o está, non temos que facer nada
        // Se non está liberada. temos que facer que a roda se quede
        // nunha das posicions prefixadas
        if (!rotacionLibre)
        {
            Snap();
        }
        else
        {
            HapticFeedback();
        }
    }

    public void Liberar(bool libre)
    {
        rotacionLibre = libre;

        baseController.SendHapticImpulse(0.3f, 0.1f);
    }

    private void Snap()
    {
        //Esta funcion busca e establece o ángulo no que debe quedar a roda
        // será o angulo mais cercano o que ten a roda no momento de soltala
        // O primerio e mirar cal e o angulo actual
        float actualAngle = transform.localEulerAngles[turnAxis];

        // Despois buscamos cal dos angulos prefixados e o mais cercano
        // Interesanos (mais adiante veremos por que) obter o indice do ángulo en snapAngles
        //en lugar do angulo en si
        int indiceAnguloDestino = BuscarAnguloMaisCercano(actualAngle);

        //Finalmente establecemos ese angulo na posicion
        SnapTo(snapAngles[indiceAnguloDestino]);
    }

    private int BuscarAnguloMaisCercano(float angle)
    {
        int angleIndex = -1;
        float actualMin = 360;

        for (int i = 0; i < snapAngles.Length; i++)
        {
            if (Mathf.Abs(angle - snapAngles[i]) < actualMin)
            {
                actualMin = Mathf.Abs(angle - snapAngles[i]);
                angleIndex = i;
            }
        }

        return angleIndex;
    }

    private void SnapTo(float angle)
    {
        Vector3 actualEulerAngles = transform.localEulerAngles;

        actualEulerAngles[turnAxis] = angle;

        transform.localEulerAngles = actualEulerAngles;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private void HapticFeedback()
    {
        float referenceAngle = snapAngles[BuscarAnguloMaisCercano(transform.localEulerAngles[turnAxis])];
        if (hapticFeedBackControl && Mathf.Abs(referenceAngle - transform.localEulerAngles[turnAxis]) > hapticFeedBackRange)
        {
            hapticFeedBackControl = false;
        }
        else if (!hapticFeedBackControl && Mathf.Abs(referenceAngle - transform.localEulerAngles[turnAxis]) < hapticFeedBackRange)
        {
            hapticFeedBackControl = true;
            baseController.SendHapticImpulse(0.3f, 0.1f);
        }
    }

    public bool CodeCheck()
    {
        float actualAngle = transform.localEulerAngles[turnAxis];
        return correctIndex == BuscarAnguloMaisCercano(actualAngle);
    }
}
