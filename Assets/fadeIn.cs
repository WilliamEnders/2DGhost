using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class fadeIn : MonoBehaviour {

    public Image img;

    void Start() {

    }
	// Use this for initialization
	public void FadeIn () {
        Fade(0f,1f,0);
    }

    public void Reload() {
        Fade(1f, 1f, 1);
    }

    public void Fade(float to, float time, int change) {
        StartCoroutine(FadeTo(to, time, change));
    }

    IEnumerator FadeTo(float aValue, float aTime, int scene) {
        float alpha = img.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            img.color = newColor;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        if (scene == 1) {
            Scene curScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(curScene.buildIndex);
            print("reloading scene!");

        }
        if (scene == 2) {
            Scene curScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(curScene.buildIndex + 1);

        }
    }

}
