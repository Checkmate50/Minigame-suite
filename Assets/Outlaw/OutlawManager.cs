using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class OutlawManager : GameManager {

    public override void startGame() {
        SceneManager.LoadScene("Outlaw_Game");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
