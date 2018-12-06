using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void playGame() {
        SceneManager.LoadScene("Escena_1");
    }

    public void quitGame() {
        Application.Quit();
    }
    
    void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
            playGame();
	}
}
