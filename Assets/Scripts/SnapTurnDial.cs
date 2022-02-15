using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapTurnDial : MonoBehaviour
{
    public delegate void codeChangedDelegate();
    public event codeChangedDelegate OnCodeChanged;
    public int correctIndex;
    //Precisamos unha
    //Lista dos ángulos nos que a roda se debe deter
    //creámola pública para poder establecer estes ángulos no
    //inspector de Unity
    public float[] snapAngles;

    //En outra variable establecemos cal é o eixo no que xira a roda
    //esa é a maneira de que podamos saber cal é o angulo de Euler que temos
    //que regular dende este script
    public int turnAxis;

    public XRBaseControllerInteractor baseController;

    //Esta variable permite á roda xirar libremente mentres está agarrada
    //e que se "pegue" a un dos ángulos pre establecidos cando se solta
    private bool rotacionLibre;

    private bool hapticFeedbackControl;
    private float hapticFeedbackRange;

    // Start is called before the first frame update
    void Start()
    {
        //La rotación de inicio está bloqueada
        rotacionLibre = false;
        hapticFeedbackRange = 2.0f;
        hapticFeedbackControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        //En cada frame comprobamos se a rotación está liberada
        //se o está, non temos que facer nada
        //Se non está liberada, temos que facer que a roda se 
        //quede nunha das posicións prefixadas
        if (!rotacionLibre)
        {
            Snap();
        }
        else
        {
            HapticFeedback();
        }
    }

    private void HapticFeedback()
    {
        float referenceAngle = snapAngles[BuscarAnguloMaisCercano(transform.localEulerAngles[turnAxis])];
        if (hapticFeedbackControl && Mathf.Abs(referenceAngle - transform.localEulerAngles[turnAxis]) > hapticFeedbackRange)
        {
            hapticFeedbackControl = false;
        }
        else if (!hapticFeedbackControl && Mathf.Abs(referenceAngle - transform.localEulerAngles[turnAxis]) < hapticFeedbackRange)
        {
            hapticFeedbackControl = true;
            baseController.SendHapticImpulse(0.3f, 0.1f);
        }
    }
    public void Liberar(bool libre)
    {
        rotacionLibre = libre;

        //Si a acción consiste en fixar a posición do selector
        //debemos invocar o evento OnCodeChanged
        if (!libre)
        {
            //Comprobamos que o evento teña suscriptores
            if (OnCodeChanged != null)
            {
                //Se os ten, invocámolo
                OnCodeChanged();
            }
        }

        baseController.SendHapticImpulse(0.3f, 0.1f);
    }

    private void Snap()
    {
        //Esta función busca e establece o ángulo no que debe quedar a roda
        //será o ángulo máis cercano ó que ten a roda no momento de soltala
        //O primeiro é mirar cal é o ángulo actual
        float actualAngle = transform.localEulerAngles[turnAxis];



        //Despois buscamos cal dos ángulos prefixados é o máis cercano
        //Interésanos (máis adiante veremos por que) obter o índice do ángulo en snapAngles
        //en lugar do ángulo en si

        int indiceAnguloDestino = BuscarAnguloMaisCercano(actualAngle);

        //Finalmente establecemos ese ángulo na posición da roda
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

    public bool CodeCheck()
    {
        if (rotacionLibre)
        {
            return false;
        }
        float actualAngle = transform.localEulerAngles[turnAxis];
        return correctIndex == BuscarAnguloMaisCercano(actualAngle);
    }
}
