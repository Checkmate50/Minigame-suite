using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PongManager : GameManager {
    protected override void enterGame() { }
    protected override void enterMenu() { }

    // Update is called once per frame
    protected void Update () {
        if (Input.GetKeyDown("escape"))
            if (SceneManager.GetActiveScene().name == game)
                endGame();
	}
}
