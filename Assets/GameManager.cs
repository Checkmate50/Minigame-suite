using UnityEngine;
using System.Collections;

public abstract class GameManager : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(this);
    }

}
