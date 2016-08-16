using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PongManager : GameManager {

    public void startGame() {
        SceneManager.LoadScene("Pong_Game");
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Destroy() {
        Debug.Log("Pong Destroyed");
    }
}
