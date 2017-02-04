using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoSingleton<SceneManager> {


    public float FadeTime = 1f;


    public void FadeToScene(string sceneName) {

    }

    // Use this for initialization
	void Start () {
	    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "_preload") {
	        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
