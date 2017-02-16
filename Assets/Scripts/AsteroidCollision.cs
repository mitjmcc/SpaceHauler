using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {

    string str = "Player";
    float pitchRange = .2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(str))
        {
            other.GetComponent<CargoHealth>().loseCargo();
            GetComponent<AudioSource>().pitch += Random.Range(-pitchRange, pitchRange);
            GetComponent<AudioSource>().Play();
        }
    }
}
