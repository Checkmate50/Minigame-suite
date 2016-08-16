using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class OutlawManager : GameManager {

    protected override void handleStartGame() {
        SceneManager.LoadScene("Outlaw_Game");
    }

    protected override void handleEndGame() {
        SceneManager.LoadScene("Outlaw_Menu");
    }

    // Use this for initialization
    void Start () {
	
	}
}
