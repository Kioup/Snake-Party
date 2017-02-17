using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {

    public int NbPlayers = 2;
    public Dictionary<string, Color> PlayersColors;
    public Dictionary<string, GameObject> Players;

    public override void Init() {
        PlayersColors = new Dictionary<string, Color>();
        Players = new Dictionary<string, GameObject>();
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
