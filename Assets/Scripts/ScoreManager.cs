using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoSingleton<ScoreManager> {

    public Dictionary<string, int> Scores;

    public override void Init() {
        ResetScores();
    }

    public void AddScoreTo(string playerName, int amount) {
        Scores[playerName] += amount;
    }

    public void RemovecoreTo(string playerName, int amount) {
        Scores[playerName] -= amount;
    }

    void Update() {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.StartsWith("Game")) {
            foreach (var pName in Scores.Keys) {
                GameObject.Find("Score" + pName).GetComponent<Text>().text = Scores[pName].ToString();
            }
        }
    }

    public void ResetScores() {
        Scores = new Dictionary<string, int>() {
            {"P1", 0},
            {"P2", 0},
            {"P3", 0},
            {"P4", 0}
        };
    }
}
