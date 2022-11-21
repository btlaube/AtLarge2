using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasScript : MonoBehaviour
{
    [SerializeField] private LevelLoaderScript levelLoader;

    void Start() {
        levelLoader = LevelLoaderScript.instance;
    }

    public void LoadGameScene() {
        levelLoader.LoadScene(1);
    }

}
