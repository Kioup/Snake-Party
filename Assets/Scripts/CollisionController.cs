using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CollisionController : MonoBehaviour {

    private Animator _animator;
    private Animator _uiAnimator;
    private PlayerManager _pm;


    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Killable")) {
            GetComponent<SnakeController>().Dead = true;
            _pm = GameObject.Find("_PlayerManager").GetComponent<PlayerManager>();
            Debug.Log(_pm.PlayersAlive.Keys.Count);
            if (_pm.PlayersAlive.Keys.Count > 1) _pm.PlayersAlive.Remove(transform.parent.name);
            Debug.Log(_pm.PlayersAlive.Keys.Count);

            _animator.SetTrigger("Death");
            if (!GameManager.instance.EndRound && _pm.PlayersAlive.Keys.Count == 1) {
                GameManager.instance.EndRound = true;
                _animator.SetTrigger("Win");
                var winner = _pm.PlayersAlive.Values.First();
                ScoreManager.instance.AddScoreTo(winner.name, 1);
                var colorForPlayer = Utils.ColorTypeConverter.ToRgbHex(winner.GetComponent<PlayerPrefs>().PlayerColor);
                var canvas = GameObject.Find("Canvas");
                canvas.GetComponent<Animator>().SetTrigger("Win");
                var txt = canvas.transform.Find("TXT_WIN").GetComponent<Text>();
                txt.text = "Le <color=" + colorForPlayer + ">joueur " + winner.name.Remove(0,1) + "</color> a gagné !";
                GameManager.instance.RestartScene();
            }
        }
    }

	// Use this for initialization
	void Start () {
	    GameManager.instance.EndRound = false;
	    _uiAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
	    _animator = Camera.main.GetComponent<Animator>();
	}

}
