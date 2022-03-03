using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceColor : MonoBehaviour
{
    public Material color;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Chapa1" || child.name == "Chapa2" || child.name == "Chapa3")
            {
                child.GetComponent<MeshRenderer>().material = color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
