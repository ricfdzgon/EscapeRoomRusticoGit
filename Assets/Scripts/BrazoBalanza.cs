using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazoBalanza : MonoBehaviour
{
    private List<SmartWeightProvider> contidoPratoEsquerdo;
    private float pesoPratoEsquerdo;
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
        pesoPratoEsquerdo = calcularPesoPratoEsquerdo();
        Debug.Log("BrazoBalanza.AddObject novo peso " + pesoPratoEsquerdo);

        //Suscribimos o evneto OnGravitingChanged do obxecto que se engadiu
        swp.OnGravitingChanged += ActualizarPesoPratoEsquerdo;
    }

    private void RemoveObject(SmartWeightProvider swp)
    {
        contidoPratoEsquerdo.Remove(swp);
        pesoPratoEsquerdo = calcularPesoPratoEsquerdo();
        Debug.Log("BrazoBalanza.RemoveObject novo peso " + pesoPratoEsquerdo);
        //DesSuscribimos o evneto OnGravitingChanged do obxecto que se retira
        swp.OnGravitingChanged -= ActualizarPesoPratoEsquerdo;
    }

    private void ActualizarPesoPratoEsquerdo()
    {
        pesoPratoEsquerdo = calcularPesoPratoEsquerdo();
        Debug.Log("BrazoBalanza.ActualizarPesoPratoEsquerdo novo peso: " + pesoPratoEsquerdo);
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
