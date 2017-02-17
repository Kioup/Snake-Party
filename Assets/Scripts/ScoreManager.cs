using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour {

    public Dictionary<string, int> Scores;



    public void Start() {
        ResetScores(GameManager.instance.NbPlayers);
    }


    public void AddScoreTo(string playerName, int amount) {
        Scores[playerName] += amount;
    }

    public void RemovecoreTo(string playerName, int amount) {
        Scores[playerName] -= amount;
    }

    void Update() {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Game")) {
            foreach (var pName in Scores.Keys) {
                var scoreGo = GameObject.Find("Score" + pName);
                if(scoreGo.activeSelf) scoreGo.GetComponent<Text>().text = Scores[pName].ToString();

            }
        }
    }

    public void ResetScores(int nbPlayers) {
        Scores = new Dictionary<string, int>();
            for (var i = 1; i <= nbPlayers; i++) {
                Scores.Add("P" + i, 0);
        }
    }
}
