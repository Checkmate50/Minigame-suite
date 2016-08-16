using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour {

    [SerializeField]
    protected string game;
    [SerializeField]
    protected string menu;

    void Awake() {
        if (GameObject.FindGameObjectsWithTag("GameController").Length >= 2)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += gameLoaded;
    }

    private void gameLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == game)
            enterGame();
        else if (scene.name == menu)
            enterMenu();
    }

    public void startGame() {
        SceneManager.LoadScene(game);
    }

    public void endGame() {
        SceneManager.LoadScene(menu);
    }

    protected abstract void enterGame();
    protected abstract void enterMenu();
}
