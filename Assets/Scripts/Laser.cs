using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : MonoBehaviour {

	public float laserAmt, warningAmt;
    GameObject laser, laserWarning;
	protected string str = "Player";
	bool laserOn;
	Timer laserTime, warningTime;

	int laserState = 0;
	
	void Awake() {
		laserTime = gameObject.AddComponent<Timer>();
		warningTime = gameObject.AddComponent<Timer>();
		laserTime.stopTimer();
		laserTime.setDefaultTimeRemaining(laserAmt);
		warningTime.setDefaultTimeRemaining(warningAmt);

		GetComponent<Collider>().enabled = false;
		laser = transform.GetChild(0).gameObject;
		laserWarning = transform.GetChild(1).gameObject;
	}

	void Update () {
		switch(laserState) {
			case 0:

				break;
			case 1:

				break;
			case 2:

				break;
			case 3:

				break;
		}
		if (warningTime.isTimeRemaining()) {
			SwitchOnLaser(false);
		} else {
			warningTime.stopTimer();
			laserTime.startTimer();
			laserTime.Reset();
		}
		if (laserTime.isTimeRemaining()) {
			SwitchOnLaser(true);
		} else {
			laserTime.stopTimer();
			warningTime.stopTimer();
			warningTime.Reset();
		}
	}

	private void SwitchOnLaser(bool on) {
		laser.SetActive(on);
		laserWarning.SetActive(!on);
		GetComponent<Collider>().enabled = on;
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(str))
        {
            other.GetComponent<CargoHealth>().loseCargo();
        }
    }
}
