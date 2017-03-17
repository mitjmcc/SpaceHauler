using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {
    private float force;
    private Vector3 pull;
    private Vector3 dir;

	// Use this for initialization
	void Start () {
        GetComponent<Collider>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pull = transform.position - other.GetComponent<Rigidbody>().position;
            force = 2000/pull.magnitude;
            dir = pull.normalized;
            other.gameObject.GetComponent<Rigidbody>().AddForce(force * dir, ForceMode.Force);
        }
    }
}
