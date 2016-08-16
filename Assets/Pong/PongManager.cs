using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PongManager : GameManager {

    protected override void handleStartGame() {
        SceneManager.LoadScene("Pong_Game");
    }

    protected override void handleEndGame() {
        SceneManager.LoadScene("Pong_Menu");
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (Input.GetKeyDown("escape"))
            if (inGame)
                endGame();
	}
}
