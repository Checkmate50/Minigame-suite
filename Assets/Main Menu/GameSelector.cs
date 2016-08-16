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
    private bool waitForLoad;
    private Scene mainMenu;

    public void selectGame() {
        string level = ddMenu.GetComponent<Dropdown>().captionText.text;
        SceneManager.LoadScene(level + "_Menu");
        waitForLoad = true;
    }

    public void exitGame() {
        Destroy(currentManager.gameObject);
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    void Awake() {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
        mainMenu = SceneManager.GetActiveScene();
        currentManager = null;
        waitForLoad = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (waitForLoad) {
            if (SceneManager.GetActiveScene() != mainMenu) {
                currentManager = GameObject.FindGameObjectWithTag("GameController");
                currentExit = (Button)GameObject.Instantiate(exitButton);
                currentExit.onClick.AddListener(this.exitGame);
                RectTransform rect = currentExit.GetComponent<RectTransform>();
                rect.SetParent(GameObject.Find("Canvas").transform, false);
                rect.localPosition = new Vector3(0f, -150f);
                waitForLoad = false;
            }
        }
    }
}
