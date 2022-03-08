using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazoBalanza : MonoBehaviour
{
    private float maxAngleRight = 8.0f;
    // private float maxAngleLeft = -8.0f;
    //Máxima diferenza de peso para que a inclinación do brazo
    //non sexa a máxima
    private float maxDiferenzaPeso = 0.4F;
    public delegate void WeigthChangedDelegate(bool pesoCorrecto);
    public static event WeigthChangedDelegate OnWeightChanged;

    private List<SmartWeightProvider> contidoPratoEsquerdo;
    private float pesoPratoEsquerdo;
    public float pesoPratoDereito;
    private float targetAngle;
    private float angularSpeed = 80;
    void Start()
    {
        contidoPratoEsquerdo = new List<SmartWeightProvider>();
        //Suscribirnos ós eventos OnObjectEnter e OnObjectExit de ZonaSensibleHandle
        ZonaSensibleHandler.OnObjectEnter += AddObject;
        ZonaSensibleHandler.OnObjectExit += RemoveObject;
        pesoPratoEsquerdo = 0;

        AxustarAnguloBrazo();
        targetAngle = maxAngleRight;
    }

    void Update()
    {
        Vector3 currentAngle = transform.localEulerAngles;
        if (currentAngle.z != targetAngle)
        {
            float step = angularSpeed * Time.deltaTime;
            if (Mathf.Abs(targetAngle - currentAngle.z) > step)
            {
                currentAngle.z += Mathf.Sign(targetAngle - currentAngle.z) * step;
            }
            else
            {
                currentAngle.z = targetAngle;
            }
            transform.localEulerAngles = currentAngle;
        }
    }

    private void AddObject(SmartWeightProvider swp)
    {
        contidoPratoEsquerdo.Add(swp);
        ActualizarPesoPratoEsquerdo();
        //Suscribimos o evneto OnGravitingChanged do obxecto que se engadiu
        swp.OnGravitingChanged += ActualizarPesoPratoEsquerdo;
    }

    private void RemoveObject(SmartWeightProvider swp)
    {
        contidoPratoEsquerdo.Remove(swp);
        ActualizarPesoPratoEsquerdo();
        //DesSuscribimos o evneto OnGravitingChanged do obxecto que se retira
        swp.OnGravitingChanged -= ActualizarPesoPratoEsquerdo;
    }

    private void ActualizarPesoPratoEsquerdo()
    {
        pesoPratoEsquerdo = calcularPesoPratoEsquerdo();
        Debug.Log("BrazoBalanza.ActualizarPesoPratoEsquerdo novo peso: " + pesoPratoEsquerdo);

        if (pesoPratoEsquerdo == pesoPratoDereito)
        {
            Debug.Log("BrazoBalanza.ActualizarPesoPratoEsquerdo PESO CORRECTO");
            if (OnWeightChanged != null)
            {
                OnWeightChanged(true);
            }
        }
        else
        {
            if (OnWeightChanged != null)
            {
                OnWeightChanged(false);
            }
        }
        AxustarAnguloBrazo();
    }

    private float calcularPesoPratoEsquerdo()
    {
        float peso = 0;

        foreach (SmartWeightProvider swp in contidoPratoEsquerdo)
        {
            peso += swp.getWeigth();
        }

        return peso;
    }

    private void AxustarAnguloBrazo()
    {
        //Ratio da diferenza de pesos respecto ó peso total
        float ratioPesos = (pesoPratoDereito - pesoPratoEsquerdo) / (pesoPratoDereito + pesoPratoEsquerdo);
        Vector3 currentEulerAngles = transform.localEulerAngles;

        if (Mathf.Abs(ratioPesos) > maxDiferenzaPeso)
        {
            //  currentEulerAngles.z = Mathf.Sign(ratioPesos) * maxAngleRight;
            targetAngle = Mathf.Sign(ratioPesos) * maxAngleRight;
        }
        else
        {
            //  currentEulerAngles.z = ratioPesos / maxDiferenzaPeso * maxAngleRight;
            targetAngle = ratioPesos / maxDiferenzaPeso * maxAngleRight;
        }
        transform.localEulerAngles = currentEulerAngles;
    }
}
