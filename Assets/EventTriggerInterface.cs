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
                if(onPointerDownEvent == null)
                {
                    onPointerDownEvent = action;
                }
                else
                {
                    onPointerDownEvent += action;
                }
                break;
        }
    }

    public static void AddEventTriggerStatic(supportedEvents triggerType, Action action, GameObject triggeringObject)
    {
        EventTriggerInterface component = triggeringObject.AddComponent<EventTriggerInterface>();
        component.AddEventTrigger(triggerType, action);
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