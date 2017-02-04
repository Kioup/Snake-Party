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

	// Use this for initialization
	void Start () {
	    _spawnPoints = new List<Transform>();
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
            var instance = Instantiate(GetNPlayerPrefab(i), _spawnPoints[index].position, Quaternion.identity);
            _spawnPoints.RemoveAt(index);
        }
    }

    private GameObject GetNPlayerPrefab(int playerNumber) {
        switch (playerNumber) {
            case 1:
                return Player1Prefab;
            case 2:
                return Player2Prefab;
            case 3:
                return Player1Prefab;
            case 4:
                return Player1Prefab;
            default:
                throw new Exception("Le numéro de joueur est invalide");
        }
    }
}
