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
    void Start()
    {
        contidoPratoEsquerdo = new List<SmartWeightProvider>();
        //Suscribirnos ós eventos OnObjectEnter e OnObjectExit de ZonaSensibleHandle
        ZonaSensibleHandler.OnObjectEnter += AddObject;
        ZonaSensibleHandler.OnObjectExit += RemoveObject;
        pesoPratoEsquerdo = 0;

        AxustarAnguloBrazo();
    }

    void Update()
    {

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
        if (Mathf.Abs(ratioPesos) > maxDiferenzaPeso)
        {
            Vector3 currentEulerAngles = transform.localEulerAngles;
            currentEulerAngles.z = Mathf.Sign(ratioPesos) * maxAngleRight;
            transform.localEulerAngles = currentEulerAngles;
        }
    }
}
