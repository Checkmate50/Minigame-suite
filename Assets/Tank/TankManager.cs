using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class TankManager : GameManager {

    protected override void handleStartGame() {
        SceneManager.LoadScene("Tank_Game");
    }

    protected override void handleEndGame() {
        SceneManager.LoadScene("Tank_Menu");
    }

    // Use this for initialization
    void Start () {
	
	}
}
