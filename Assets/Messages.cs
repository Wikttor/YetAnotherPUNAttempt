using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Messages : MonoBehaviour
{
    public TextMeshProUGUI myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = this.GetComponent<TextMeshProUGUI>();
        if (myText == null)
        {
            Debug.Log("Couldn't get TMProUI component");
        }
    }

    public void PUNConnectedSuccessfully()
    {
        myText.text += "\nConnected to server and joined lobby successfully";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
