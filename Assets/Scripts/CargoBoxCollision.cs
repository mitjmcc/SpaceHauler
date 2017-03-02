﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoBoxCollision : MonoBehaviour {

    string str = "Player";
    float pitchRange = .2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(str))
        {
            other.GetComponent<CargoHealth>().gainCargo();
            other.GetComponent<ShakeObject>().Shake((other.transform.position - transform.position).normalized);
            GetComponent<AudioSource>().pitch += Random.Range(-pitchRange, pitchRange);
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, .8f);
        }
    }
}
