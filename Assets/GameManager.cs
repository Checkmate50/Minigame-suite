using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour {

    protected bool inGame;
    private static bool waitForGame;
    private static bool waitForMenu;
    private Scene oldScene;

    protected abstract void handleStartGame();
    protected abstract void handleEndGame();

    void Awake() {
        if (GameObject.FindGameObjectsWithTag("GameController").Length >= 2)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);
        waitForMenu = false;
    }

    public void startGame() {
        oldScene = SceneManager.GetActiveScene();
        waitForGame = true;
        handleStartGame();
    }

    public void endGame() {
        oldScene = SceneManager.GetActiveScene();
        waitForMenu = true;
        handleEndGame();
    }

    protected virtual void enterGame() {
        Debug.Log("In game");
        inGame = true;
        waitForGame = false;
    }

    protected virtual void enterMenu() {
        inGame = false;
        waitForMenu = false;
    }

    protected virtual void Update() {
        if (waitForGame) 
            if (SceneManager.GetActiveScene() != oldScene)
                enterGame();
        if (waitForMenu)
            if (SceneManager.GetActiveScene() != oldScene)
                enterMenu();
    }

}
