using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControlScript : MonoBehaviour
{

    public GameObject[] colorDials;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CodeCheck()) {
            Open();
        }
    }

    private bool CodeCheck() {
        for(int i=0; i<colorDials.Length; i++) {
            if (colorDials[i].GetComponentInChildren<SnapTurnDial>() == null) {
                Debug.Log("DoorControlScript.Open null SpanTurnDial " + i);
                return false;
            }
            if( colorDials[i].GetComponentInChildren<SnapTurnDial>().CodeCheck() == false) {
                return false;
            }
        }
        return true;

    }

    private void Open() {
        Debug.Log("DoorControlScript.Open A abriríase se souperamos como");
    }
}
