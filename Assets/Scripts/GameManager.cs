using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {

    public int NbPlayers = 2;
    public List<Color> AvailableColors;
    public bool ArePlayerAlreadyChosen;
    public bool EndRound;


    [Serializable]
    public struct PlayerInformations {
        public string PlayerName;
        public Color PlayerColor;
    }

    public Dictionary<string, PlayerInformations> PlayerInfo;


    public override void Init() {
        EndRound = false;
        ArePlayerAlreadyChosen = false;
        AvailableColors = AvailableColors ?? new List<Color>{Color.red, Color.green, Color.yellow, Color.cyan};
        PlayerInfo = new Dictionary<string, PlayerInformations>();
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
