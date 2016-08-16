using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ZappersManager : GameManager {

    public override void startGame() {
        SceneManager.LoadScene("Zappers_Game");
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
