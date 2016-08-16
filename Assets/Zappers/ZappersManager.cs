using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ZappersManager : GameManager {

    protected override void handleStartGame() {
        SceneManager.LoadScene("Zappers_Game");
    }

    protected override void handleEndGame() {
        SceneManager.LoadScene("Zappers_Menu");
    }

    // Use this for initialization
    void Start () {
	}
}
