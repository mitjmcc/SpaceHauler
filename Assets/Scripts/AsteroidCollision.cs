using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {

    protected string str = "Player";
    protected float pitchRange = .2f;

    private void Awake()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(str))
        {
            other.GetComponent<CargoHealth>().loseCargo();
            other.GetComponent<ShakeObject>().Shake((other.transform.position - transform.position).normalized);
            GetComponent<AudioSource>().pitch += Random.Range(-pitchRange, pitchRange);
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, .1f);
        }
    }
}
