using System.Collections;
using System.Collections.Generic;
using TeamUtility.IO;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour {
	
	void FixedUpdate () {
		if (InputManager.GetButtonDown("Submit"))
        {
            Cast();
        }
	}

    void Cast()
    {
        RaycastHit hit;
        GameObject o;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 2f))
        {
            //.GetComponent<CameraRaycastTarget>()
            o = hit.transform.gameObject;
            if (o != null)
            {
                o.GetComponent<CameraRaycastTarget>().Trigger();
            }
        }
    }
}
