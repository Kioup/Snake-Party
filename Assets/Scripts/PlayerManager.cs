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
    public List<Color> AvailableColors;

    public List<Transform> SpawnPoints;
    public Dictionary<string, GameObject> PlayersAlive;

    [Serializable]
    public struct PlayerInput {
        public string PlayerName;
        public string LeftKey;
        public string RightKey;
    }

    public PlayerInput[] PlayersInputs;


    public void Start() {
        SpawnPoints = new List<Transform>();
	    PlayersAlive = new Dictionary<string, GameObject>();
	    foreach(GameObject go in GameObject.FindGameObjectsWithTag("Respawn"))
	    {
            SpawnPoints.Add(go.transform);
	    }
		SpawnPlayers(GameManager.instance.NbPlayers);
    }

    public void SpawnPlayers(int nbPlayers) {
        for (var i = 1; i <= nbPlayers; i++) {
            var index = Random.Range(0, SpawnPoints.Count - 1);
            GameObject instance;
            if (GameManager.instance.NbPlayers != GameManager.instance.Players.Count) {
                instance = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            }
            else {
                instance = Instantiate(GameManager.instance.Players["P" + i], Vector3.zero, Quaternion.identity);
            }
            instance.name = "P" + i;
            instance.GetComponent<PlayerPrefs>().PlayerName = instance.name;
            var pColor = ExtensionsMethods.Random(ref AvailableColors);
            if (GameManager.instance.NbPlayers != GameManager.instance.PlayersColors.Count) {
                GameManager.instance.PlayersColors.Add(instance.name, pColor);
            }

            instance.GetComponent<PlayerPrefs>().PlayerColor = GameManager.instance.PlayersColors[instance.name];
            var head = instance.transform.FindChild("Head");
            head.GetComponent<SnakeController>().Dead = true;
            head.position = SpawnPoints[index].position;
            head.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0,361)));
            if (GameManager.instance.NbPlayers != GameManager.instance.Players.Count) {
                GameManager.instance.Players.Add(instance.name, instance);
            }
            SpawnPoints.RemoveAt(index);
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

    public void RemovePlayer(string playerName) {
        PlayersAlive.Remove(playerName);
    }

    public IEnumerator StartGame(float delay = 2f) {
        Camera.main.orthographicSize = 5f;
        Camera.main.transform.position = new Vector3(0, 0, -11);

        yield return new WaitForSeconds(delay);
        foreach (var player in GameManager.instance.Players) {
            PlayersAlive.Add(player.Key, player.Value);
        }
        foreach (var player in PlayersAlive.Values) {
            var head = player.transform.FindChild("Head");
            head.GetComponent<SnakeController>().Dead = false;
            Destroy(head.transform.FindChild("arrow").gameObject);
        }

    }

//    void Update() {
//        if (PlayersAlive.Keys.Count == 1) {
//            var lastPlayerPosition = PlayersAlive.Values.First().transform.Find("Head").position;
//            Camera.main.orthographicSize = 4f;
//            Camera.main.transform.position = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y, -11);
//        }
//    }

   }
