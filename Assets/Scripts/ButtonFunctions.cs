using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonFunctions : MonoBehaviour {
    public void StartGame() {
        Debug.Log("Loading game scene...");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    public void QuitGame(){
        Application.Quit();
    }

    
}
