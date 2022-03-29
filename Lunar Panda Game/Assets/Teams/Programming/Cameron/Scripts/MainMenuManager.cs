using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This is just a script where u can put whatever u want for the main menu. At the moment its pretty small but if anything needs to get done in the main menu either at the start, or every 
frame, this is probably the best place to put it.
*/

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        //This stuff is really important. If this isnt present, when the cabin is finished and the player goes back to the main menu, the cursor is still locked
        //in short, DO NOT REMOVE THIS OR SHIT WILL BREAK
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
