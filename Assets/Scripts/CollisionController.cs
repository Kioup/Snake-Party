using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

public class CollisionController : MonoBehaviour {

    private Animator _animator;
    private Animator _fadeAnim;
    private Animator _uiAnimator;
    private PlayerManager _pm;



    // TODO: Faire en sorte que lorsque la partie est fini, transition au noir, on reset les positions des joueurs etc et on ré affice le jeu, sans redémarrer la scène

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Killable")) {
            GetComponent<SnakeController>().Dead = true;
            _pm = GameObject.Find("_PlayerManager").GetComponent<PlayerManager>();
            _pm.RemovePlayer(transform.parent.name);
            _animator.SetTrigger("Death");
            if (_pm.PlayersAlive.Keys.Count == 1) {
                StartCoroutine(OnWin());
            }

            Debug.Log(_pm.PlayersAlive.Keys.Count + " joueurs restants");


        }
    }

	// Use this for initialization
	void Start () {
	    _uiAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
	    _fadeAnim = GameObject.FindGameObjectWithTag("Fader").GetComponent<Animator>();
	    _animator = Camera.main.GetComponent<Animator>();
	}

    IEnumerator OnWin() {
        _animator.SetTrigger("Win");
        var winner = _pm.PlayersAlive.Values.First();
        GameObject.Find("_ScoreManager").GetComponent<ScoreManager>().AddScoreTo(winner.name, 1);
        var colorForPlayer = Utils.ColorTypeConverter.ToRgbHex(winner.GetComponent<PlayerPrefs>().PlayerColor);
        var canvas = GameObject.Find("Canvas");
        canvas.GetComponent<Animator>().SetTrigger("Win");
        var txt = canvas.transform.Find("TXT_WIN").GetComponent<Text>();
        txt.text = "Le <color=" + colorForPlayer + ">joueur " + winner.name.Remove(0,1) + "</color> a gagné !";
        yield return new WaitForSeconds(4f);
        _fadeAnim.Play("FadeIn");
        yield return new WaitForSeconds(1f);
        ResetScene();
        yield return new WaitForSeconds(1f);
        _fadeAnim.Play("FadeOut");
        StartCoroutine(_pm.StartGame());

    }

    private void ResetScene() {
        var spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        var players = GameObject.FindGameObjectsWithTag("Player");
        _pm.SpawnPoints = new List<Transform>();
        foreach (var spawnPoint in spawnPoints) {
            _pm.SpawnPoints.Add(spawnPoint.transform);
        }
        foreach (var player in players) {
            Destroy(player);
        }
//        foreach (var player in GameManager.instance.Players) {
//            var instance = Instantiate(player.Value, Vector3.zero, Quaternion.identity);
//            instance.name = player.Key;
//            var head = instance.transform.FindChild("Head");
//            head.GetComponent<SnakeController>().Dead = true;
//            head.position = ExtensionsMethods.Random(ref _pm.SpawnPoints).position;
//            head.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0,361)));
//            instance.transform.FindChild("Tail").GetComponent<LineRenderer>().SetPositions(new []{head.position, head.position});
//        }


    }
}
