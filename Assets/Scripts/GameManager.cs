using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {

    public int NbPlayers = 2;

    public override void Init() {
        Debug.Log("Instance created");
    }



    // Use this for initialization
    private void Start () {
	}


	
	// Update is called once per frame
    private void Update () {
		
	}
}
