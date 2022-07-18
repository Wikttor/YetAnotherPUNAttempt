using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsOnCallCannotCallStaticFunctions : MonoBehaviour
{
    public void CallStaticFuncion()
    {
        if (MyPUNBacis.joinedRoom)
        {
            FirstRPCCarrier.ButtonPressed();
        }
        else
        {
            Messages.DisplayMessage("You are not connected, button won't do a thing");
        }
    }
}
