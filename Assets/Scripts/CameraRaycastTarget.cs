using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraRaycastTarget : MonoBehaviour {

    public UnityEvent events;

    public void Trigger()
    {
        events.Invoke();
    }
}
