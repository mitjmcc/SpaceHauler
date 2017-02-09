using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	void Start () {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
