using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerInterface : EventTrigger
{
    public enum supportedEvents
    {
        OnPointerDown
    }
    private event Action onPointerDownEvent;
    public void AddEventTrigger(supportedEvents triggerType, Action action)
    {
        switch (triggerType)
        {
            case supportedEvents.OnPointerDown:
                onPointerDownEvent = action;
                break;
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (onPointerDownEvent != null)
        {
            onPointerDownEvent();
        }
    }
}