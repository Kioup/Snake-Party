using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoSingleton<SceneManager> {


    public float FadeTime = 1f;

    public int NumberOfLevels = 1;

    private Animator _fadeAnim;

    public void FadeToScene(string sceneName, float speed = 1f) {
        StartCoroutine(FadeToSceneLoop(sceneName, speed));
    }

    IEnumerator FadeToSceneLoop(string sceneName, float speed) {
        _fadeAnim.speed = speed;
        _fadeAnim.Play("FadeIn");
//        UnityEngine.SceneManagement.SceneManager.SetActiveScene(
//            UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName));
        yield return new WaitForSeconds(0.7f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

//    // Use this for initialization
//	void Start () {
//	    UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
//	    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "_preload") {
//	        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
//	    }
//	}

    public override void Init() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "_preload") {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode) {
        if (GameObject.FindGameObjectWithTag("Fader")) {
            Debug.Log("Fader trouvé");
            _fadeAnim = GameObject.FindGameObjectWithTag("Fader").GetComponent<Animator>();
            _fadeAnim.Play("FadeOut");

        }
    }

}

