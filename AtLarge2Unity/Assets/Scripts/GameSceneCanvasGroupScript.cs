using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneCanvasGroupScript : MonoBehaviour {
    
    [SerializeField] private LevelLoaderScript levelLoader;

    void Start() {
        levelLoader = LevelLoaderScript.instance;
    }

    void Update() {
        if (Input.GetKey(KeyCode.P)) {
            OpenPauseMenu();
        }
    }

    public void LoadMainMenu() {
        Time.timeScale = 1f;
        levelLoader.LoadScene(0);
    }

    public void LoadGameScene() {
        for (int i = 1; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    public void OpenPauseMenu() {
        Time.timeScale = 0f;
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void ClosePauseMenu() {
        Time.timeScale = 1f;
        transform.GetChild(3).gameObject.SetActive(false);
    }

}
