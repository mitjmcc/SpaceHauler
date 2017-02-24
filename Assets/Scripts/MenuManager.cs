using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject mainScreen;
    public GameObject optionsScreen;

	void Start () {
        Cursor.lockState = CursorLockMode.None;
        mainScreen.SetActive(true);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Options()
    {
        mainScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void Back()
    {
        mainScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }
}
