using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratoBalanza : MonoBehaviour
{
    public Transform attachTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = attachTransform.position;
        transform.rotation = Quaternion.identity;
    }
}
