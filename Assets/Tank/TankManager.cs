using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class TankManager : GameManager {

    public override void startGame() {
        SceneManager.LoadScene("Tank_Game");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
