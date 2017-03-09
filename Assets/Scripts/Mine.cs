using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    Collider target;
    bool chase;
	// Use this for initialization
	private void Awake () {
        GetComponent<Collider>().enabled = true;
	}

    private void Update () {
        if (chase)
        {
            transform.LookAt(target.gameObject.transform.position);
            transform.Translate(Vector3.forward * .1f);
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chase = true;
            target = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chase = false;
        }
    }
}
