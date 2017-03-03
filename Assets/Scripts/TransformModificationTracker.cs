using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformModificationTracker : MonoBehaviour {

    private Vector3 posPrev;
    private Quaternion rotPrev;
    private float torPrev;
    private float spdPrev;

    void OnEnable()
    {
        posPrev = transform.localPosition;
        rotPrev = transform.localRotation;
    }

    public bool CustomUpdate()
    {
        transform.localScale = Vector3.one;

        if (posPrev != transform.localPosition
            || rotPrev != transform.localRotation)
        {
            posPrev = transform.localPosition;
            rotPrev = transform.localRotation;
            return true;
        }

        return false;
    }
}
