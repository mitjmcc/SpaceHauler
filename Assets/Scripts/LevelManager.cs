using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public Text GameOver, Restart;

    GameObject Player;
    Scene menu;
    bool gameover;

    void Start () {
        instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {
        // Needs to be moved to an animation
        if (gameover && Input.GetKeyDown("space"))
        {
            backToMenu();
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void gameOver()
    {
        Player.GetComponent<TruckController>().shutdown();
        GameOver.gameObject.SetActive(true);
        Restart.gameObject.SetActive(true);
        gameover = true;
    }
}
