using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class skipScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex + 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
