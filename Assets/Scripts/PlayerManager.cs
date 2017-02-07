using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour {

    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject Player3Prefab;
    public GameObject Player4Prefab;

    private List<Transform> _spawnPoints;
    public Dictionary<string, GameObject> Players;

    public void Start() {
        _spawnPoints = new List<Transform>();
	    Players = new Dictionary<string, GameObject>();
	    foreach(GameObject go in GameObject.FindGameObjectsWithTag("Respawn"))
	    {
            _spawnPoints.Add(go.transform);
	    }
		SpawnPlayers(GameManager.instance.NbPlayers);
    }

    private void SpawnPlayers(int nbPlayers) {
        for (var i = 1; i <= nbPlayers; i++) {
            var index = Random.Range(0, _spawnPoints.Count - 1);
            var instance = Instantiate(GetNPlayerPrefab(i), Vector3.zero, Quaternion.identity);
            instance.name = "P" + i;
            GameObject.Find("Score" + instance.name).SetActive(true);
            var head = instance.transform.FindChild("Head");
            head.GetComponent<SnakeController>().Dead = true;
            head.position = _spawnPoints[index].position;
            head.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0,361)));
            Players.Add("P" + i, instance);
            _spawnPoints.RemoveAt(index);
        }
        StartCoroutine(StartGame());
    }





    public void RemovePlayer(string playerName) {
        Players.Remove(playerName);
    }

    IEnumerator StartGame(float delay = 2f) {
        yield return new WaitForSeconds(delay);
        foreach (var player in Players.Values) {
            player.transform.FindChild("Head").GetComponent<SnakeController>().Dead = false;
        }

    }

    void Update() {
        if (Players.Keys.Count == 1) {
            var lastPlayerPosition = Players.Values.First().transform.Find("Head").position;
            Camera.main.orthographicSize = 4f;
            Camera.main.transform.position = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y, -10);
        }
    }

    public string GetColorForPlayer(string playerName) {
        return ColorTypeConverter.ToRgbHex(Players[playerName]
            .transform.Find("Tail")
            .GetComponent<LineRenderer>()
            .material.color);

    }

    private GameObject GetNPlayerPrefab(int playerNumber) {
        switch (playerNumber) {
            case 1:
                return Player1Prefab;
            case 2:
                return Player2Prefab;
            case 3:
                return Player3Prefab;
            case 4:
                return Player4Prefab;
            default:
                throw new Exception("Le numéro de joueur est invalide");
        }
    }
}
