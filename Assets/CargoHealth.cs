using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoHealth : MonoBehaviour {

    //Might move this
    public Image[] cargoImages;
    public Text GameOver;

    int cargo;

	// Use this for initialization
	void Start () {
        cargo = cargoImages.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (cargo <= 0)
        {
            gameOver();
        }
	}

    public void loseCargo()
    {
        if (cargo > 0)
        {
            cargoImages[cargo - 1].gameObject.SetActive(false);
            cargo--;

        }
    }

    public void gameOver()
    {
        GameOver.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
