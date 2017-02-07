using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CargoHealth : MonoBehaviour {

    //Might move this
    public Image[] cargoImages;
    public Text GameOver, Restart;

    int cargo;

	// Use this for initialization
	void Start () {
        cargo = cargoImages.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (cargo == 0)
        {
            gameOver();
        }
        if (cargo == -1 && Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(0);
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
        cargo = -1;
        GameOver.gameObject.SetActive(true);
        Restart.gameObject.SetActive(true);
        GetComponent<TruckController>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
