using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerChanging : MonoBehaviour
{
    public int id;
    //public int powerOffID;
    
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.powerOff += powerOff;
        GameEvents.current.powerOn += powerOn;
    }

    public void powerOn(int id)
    {
        if (id == this.id)
        {
            //print("Can No interact");
            GetComponent<Interaction>().canInteract = true;
        }
    }

    public void powerOff(int id)
    {
        if (id == this.id)
        {
            //print("Can interact");
            GetComponent<Interaction>().canInteract = false;
        }
    }
}
