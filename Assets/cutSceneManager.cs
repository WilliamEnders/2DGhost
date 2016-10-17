using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class cutSceneManager : MonoBehaviour {

    public generalMovement player;
    private cameraMove cam;
    public int currentFrame;
    public bool isCutScene;
    private Text txt;

    public cutScene sceneInfo;
    private dontDestroyInfo info;

	// Use this for initialization
	void Start () {
        isCutScene = false;
        txt = GetComponentInChildren<Text>();
        cam = GetComponent<cameraMove>();
        info = GetComponent<dontDestroyInfo>();
       
	}

    public void StartCutScene() {
        isCutScene = true;
        if (cam.player) {
            cam.player.GetComponent<generalMovement>().move.canMove = false;
        }
        currentFrame = 0;
    }

    void EndCutScene() {
        if (player) {
            player.move.canMove = true;
            cam.player = player.transform;
        }
        enabled = false;
        txt.transform.parent.gameObject.SetActive(false);
        info.skipCut = true;
    }

    // Update is called once per frame
    void Update() {
        if (isCutScene) {
            cam.player = sceneInfo.cut[currentFrame].focus;
            txt.text = sceneInfo.cut[currentFrame].talk;
            if (Input.GetButtonDown("Fire1")) {
                currentFrame++;
            }
            if (currentFrame >= sceneInfo.cut.Length) {
                isCutScene = false;
                EndCutScene();
            }
        }
    }
}
