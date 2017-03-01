using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public string tutorial;
    public GameObject mainScreen;
    public GameObject optionsScreen;
    public GameObject qualityScreen;
    public GameObject loadingScreen;
    public GameObject levelsScreen;

	void Start () {
        Cursor.lockState = CursorLockMode.None;
        mainScreen.SetActive(true);
    }

    public void Tutorial()
    {
        levelsScreen.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(tutorial);
    }

    public void LevelSelect()
    {
        mainScreen.SetActive(false);
        levelsScreen.SetActive(true);
    }

    public void Options()
    {
        mainScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void Quality()
    {
        optionsScreen.SetActive(false);
        qualityScreen.SetActive(true);
    }

    public void toMainMenu()
    {
        mainScreen.SetActive(true);
        optionsScreen.SetActive(false);
        levelsScreen.SetActive(false);
    }

    public void toOptions()
    {
        qualityScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
