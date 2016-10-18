using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class dontDestroyInfo : MonoBehaviour {

    public Transform focus;
    public bool skipCut;

    private cameraMove camMove;
    private fadeIn fade;
    private cutSceneManager cutScene;


    // Use this for initialization
    void Start() {
       // skipCut = false;
        DontDestroyOnLoad(gameObject);
        camMove = GetComponent<cameraMove>();
        fade = GetComponent<fadeIn>();
        cutScene = GetComponent<cutSceneManager>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            fade.Reload();
        }
    }

    void OnEnable() {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {

        fade.FadeIn();

        if (GameObject.Find("Player1")) {
            camMove.player = GameObject.Find("Player1").transform;
            camMove.rb = camMove.player.GetComponent<Rigidbody2D>();
            cutScene.player = GameObject.Find("Player1").GetComponent<generalMovement>();
        }

        if (!skipCut) {
            if (GameObject.Find("CutScene")) {
                cutScene.sceneInfo = GameObject.Find("CutScene").GetComponent<cutScene>();
                cutScene.StartCutScene();
            } else {
                cutScene.isCutScene = false;
            }
        }
    }
}
