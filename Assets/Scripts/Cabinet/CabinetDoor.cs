using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    private bool locked;
    private Vector3 initPosition;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        locked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
        {
            transform.position = initPosition;
        }
    }
    public void Lock(bool isLocket)
    {
        this.locked = isLocket;
    }
}
