using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameSelector : MonoBehaviour {

    [SerializeField]
    Button exitButton;
    [SerializeField]
    private GameObject ddMenu;

    private Dictionary<string, GameManager> managers;
    private GameObject currentManager;
    private Button currentExit;

    public void selectGame() {
        string level = ddMenu.GetComponent<Dropdown>().captionText.text;
        SceneManager.LoadScene(level + "_Menu");
    }

    public void exitGame() {
        SceneManager.LoadScene(0);
        Destroy(currentManager.gameObject);
        Destroy(gameObject);
    }

    void Awake() {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += gameLoaded;
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= gameLoaded;
    }

	// Use this for initialization
	void Start () {
        currentManager = null;
	}

    private void gameLoaded(Scene scene, LoadSceneMode mode) {
        currentManager = GameObject.FindGameObjectWithTag("GameController");
        currentExit = Instantiate(exitButton);
        currentExit.onClick.AddListener(this.exitGame);
        RectTransform rect = currentExit.GetComponent<RectTransform>();
        rect.SetParent(GameObject.Find("Canvas").transform, false);
        rect.localPosition = new Vector3(0f, -150f);
    }
}
