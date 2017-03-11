using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public Text Restart, Dialogue;
    public GameObject GameOver, loadingScreen, DialogueManager;

    GameObject Player;
    Scene menu;
    bool gameover;

    void Start () {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
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
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backToMenu()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }

    public void gameOver()
    {
        Player.GetComponent<TruckController>().shutdown();
        StartCoroutine(DelayedAnimation(Player.
            GetComponentInChildren<Camera>().GetComponent<Animation>(),
            "DollyIn", 1f));
        StartCoroutine(gameOverGUI(2f));
        Dialogue.gameObject.SetActive(false);
        DialogueManager.SetActive(false);

        gameover = true;
    }

    private IEnumerator DelayedAnimation(Animation a, string anim, float delay)
    {
        yield return new WaitForSeconds(delay);
        a.Play(anim, PlayMode.StopAll);
    }

    private IEnumerator gameOverGUI(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
