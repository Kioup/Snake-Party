using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour {

    public GameObject PlayerPrefab;

    private List<Transform> _spawnPoints;
    public Dictionary<string, GameObject> PlayersAlive;

    [Serializable]
    public struct PlayerInput {
        public string PlayerName;
        public string LeftKey;
        public string RightKey;
    }

    public PlayerInput[] PlayersInputs;


    public void Start() {
        _spawnPoints = new List<Transform>();
        PlayersAlive = new Dictionary<string, GameObject>();
	    foreach(GameObject go in GameObject.FindGameObjectsWithTag("Respawn"))
	    {
            _spawnPoints.Add(go.transform);
	    }
		SpawnPlayers(GameManager.instance.NbPlayers);
    }

    private void SpawnPlayers(int nbPlayers) {
        if (!GameManager.instance.ArePlayerAlreadyChosen) {
            for (var i = 1; i <= nbPlayers; i++) {
                var index = Random.Range(0, _spawnPoints.Count - 1);
                var instance = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
                instance.name = "P" + i;
                instance.GetComponent<PlayerPrefs>().PlayerName = instance.name;
                instance.GetComponent<PlayerPrefs>().PlayerColor =
                ExtensionsMethods.Random(ref GameManager.instance.AvailableColors);
                var head = instance.transform.FindChild("Head");
                head.GetComponent<SnakeController>().Dead = true;
                var pInfos = new GameManager.PlayerInformations();
                pInfos.PlayerColor = instance.GetComponent<PlayerPrefs>().PlayerColor;
                pInfos.PlayerName = instance.GetComponent<PlayerPrefs>().PlayerName;
                GameManager.instance.PlayerInfo.Add(instance.name, pInfos);
                PlayersAlive.Add(instance.name, instance);
                head.position = _spawnPoints[index].position;
                head.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361)));
                _spawnPoints.RemoveAt(index);
            }
            GameManager.instance.ArePlayerAlreadyChosen = true;

        }
        else {
            foreach (var pInfo in GameManager.instance.PlayerInfo.OrderBy(pair => pair.Key)) {
                var index = Random.Range(0, _spawnPoints.Count - 1);
                var instance = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
                instance.name = pInfo.Key;
                instance.GetComponent<PlayerPrefs>().PlayerName = instance.name;
                instance.GetComponent<PlayerPrefs>().PlayerColor = pInfo.Value.PlayerColor;
                var head = instance.transform.FindChild("Head");
                head.GetComponent<SnakeController>().Dead = true;
                PlayersAlive.Add(instance.name, instance);
                head.position = _spawnPoints[index].position;
                head.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361)));
                _spawnPoints.RemoveAt(index);
            }
        }
        foreach (var player in GameManager.instance.PlayerInfo.OrderBy(pair => pair.Key)) {
            Debug.Log("Info sur " + player.Value.PlayerName + " : Couleur : " + player.Value.PlayerColor);
        }

        switch (GameManager.instance.NbPlayers) {
            case 2:
                GameObject.Find("ScoreP1").SetActive(true);
                GameObject.Find("ScoreP2").SetActive(true);
                GameObject.Find("ScoreP3").SetActive(false);
                GameObject.Find("ScoreP4").SetActive(false);
                break;
            case 3:
                GameObject.Find("ScoreP1").SetActive(true);
                GameObject.Find("ScoreP2").SetActive(true);
                GameObject.Find("ScoreP3").SetActive(true);
                GameObject.Find("ScoreP4").SetActive(false);
                break;
            case 4:
                GameObject.Find("ScoreP1").SetActive(true);
                GameObject.Find("ScoreP2").SetActive(true);
                GameObject.Find("ScoreP3").SetActive(true);
                GameObject.Find("ScoreP4").SetActive(true);
                break;

        }
        StartCoroutine(StartGame());
    }


    IEnumerator StartGame(float delay = 2f) {
        yield return new WaitForSeconds(delay);
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players) {
            var head = player.transform.FindChild("Head");
            head.GetComponent<SnakeController>().Dead = false;
            Destroy(head.transform.FindChild("arrow").gameObject);
        }

    }

    void Update() {
        if (PlayersAlive.Keys.Count == 1) {
            var lastPlayerPosition = PlayersAlive.Values.First().transform.Find("Head").position;
            Camera.main.orthographicSize = 4f;
            Camera.main.transform.position = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y, -10);
        }
    }

   }
