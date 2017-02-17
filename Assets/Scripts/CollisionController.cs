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

    // TODO: Faire en sorte que lorsque la partie est fini, transition au noir, on reset les positions des joueurs etc et on ré affice le jeu, sans redémarrer la scène

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Killable")) {
            GetComponent<SnakeController>().Dead = true;
            _pm = GameObject.Find("_PlayerManager").GetComponent<PlayerManager>();
            _pm.RemovePlayer(transform.parent.name);
            _animator.SetTrigger("Death");
            if (_pm.PlayersAlive.Keys.Count == 1) {
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

            Debug.Log(_pm.PlayersAlive.Keys.Count + " joueurs restants");


        }
    }

	// Use this for initialization
	void Start () {
	    _uiAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
	    _animator = Camera.main.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
