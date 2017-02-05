using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour {

    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject Player3Prefab;
    public GameObject Player4Prefab;

    private List<Transform> _spawnPoints;
    private List<GameObject> _players;

	// Use this for initialization
	void Start () {
	    _spawnPoints = new List<Transform>();
	    _players = new List<GameObject>();
	    foreach(GameObject go in GameObject.FindGameObjectsWithTag("Respawn"))
	    {
            _spawnPoints.Add(go.transform);
	    }
		SpawnPlayers(GameManager.instance.NbPlayers);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SpawnPlayers(int nbPlayers) {
        for (var i = 1; i <= nbPlayers; i++) {
            var index = Random.Range(0, _spawnPoints.Count - 1);
            var instance = Instantiate(GetNPlayerPrefab(i), Vector3.zero, Quaternion.identity);
            _players.Add(instance);
            instance.name = "P" + i;
            var head = instance.transform.FindChild("Head");
            head.GetComponent<SnakeController>().Dead = true;
            head.position = _spawnPoints[index].position;
            head.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0,361)));
            _spawnPoints.RemoveAt(index);
        }
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame(float delay = 2f) {
        yield return new WaitForSeconds(delay);
        foreach (var player in _players) {
            player.transform.FindChild("Head").GetComponent<SnakeController>().Dead = false;
        }
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
