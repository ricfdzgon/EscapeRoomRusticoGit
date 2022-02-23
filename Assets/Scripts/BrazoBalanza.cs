using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazoBalanza : MonoBehaviour
{
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
}
