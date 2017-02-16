using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {

    public int NbPlayers = 2;

    public override void Init() {

    }


    public void RestartScene() {
        StartCoroutine(RestartSceneLoop());
    }

    IEnumerator RestartSceneLoop() {
        yield return new WaitForSeconds(4f);
        SceneManager.instance.FadeToScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

    // Use this for initialization
    private void Start () {
	}


	
	// Update is called once per frame
    private void Update () {
		
	}
}
