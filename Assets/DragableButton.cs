using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableButton : MonoBehaviour
{
    public List<GameObject> targets;
    public RectTransform[] rectTransforms;

    public void OnEnable()
    {
        if(targets == null)
        {
            targets = new List<GameObject>();
        }
        if (!targets.Contains(this.transform.parent.gameObject))
        {
            targets.Add(this.gameObject);
        }
        
        
        rectTransforms = new RectTransform[targets.Count];
        for( int i = 0; i < targets.Count; i++)
        {
            rectTransforms[i] = targets[i].GetComponent<RectTransform>();
        }
 
        this.gameObject.AddComponent<EventTriggerInterface>();
        EventTriggerInterface ETReference = this.GetComponent<EventTriggerInterface>();
        ETReference.AddEventTrigger(EventTriggerInterface.supportedEvents.OnPointerDown, StartMoving);
    }

    public void StartMoving()
    {
        StartCoroutine(KeepMoving());
    }

    IEnumerator KeepMoving()
    {
        Vector3 lastMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 mouseDelta = new Vector3(0f, 0f, 0f);
        Vector3 newMousePositon;

        while (Input.GetMouseButton(0))
        {
            newMousePositon = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            mouseDelta = newMousePositon - lastMousePosition;
            lastMousePosition = newMousePositon;
            foreach(GameObject target in targets)
            {
                target.transform.Translate(mouseDelta);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}



