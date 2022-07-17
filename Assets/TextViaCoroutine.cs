using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextViaCoroutine : MonoBehaviour
{
    public TextMeshProUGUI proTextMesh;
    public static TextViaCoroutine singleton;

     void Start()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }


}
