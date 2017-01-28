using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Killable")) {
            GetComponent<SnakeController>().Dead = true;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
