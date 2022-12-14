using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<object> { }
public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;

    public CustomGameEvent response;

    private void OnEnable()
    {
        gameEvent.RegistreListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregistreListener(this);
    }

    public void OnEventRaised(object data)
    {
        response.Invoke(data);
    }
}
