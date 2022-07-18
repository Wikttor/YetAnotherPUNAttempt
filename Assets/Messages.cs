using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Messages : MonoBehaviour
{
    public static Messages instance;
    public TextMeshProUGUI myText;

    void Start()
    {
        instance = this;
        myText = this.GetComponent<TextMeshProUGUI>();
        if (myText == null)
        {
            Debug.Log("Couldn't get TMProUI component");
        }
    }

    public static void DisplayMessage(string message)
    {
        if (instance != null)
        {
            instance.myText.text += "\n" + message;
        }
        else
        {
            Debug.Log("There is no Messages class instance");
        }
    }

    public static void PUNConnectedSuccessfully()
    {
        DisplayMessage("Connected to server and joined lobby successfully");
    }
    public static void PUNCreatedRoom()
    {
        DisplayMessage("Successfully created a room");
    }
    public static void PUNJoinedRoom()
    {
        DisplayMessage("Succesfully joined a room");
    }
}

