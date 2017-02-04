using System.Collections.Generic;
using System.Linq;
using Delegates;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    public float Speed = 1;
    public float RotationSpeed = 3;

    public bool Dead;

    private EdgeCollider2D _edgeCollider;

    private float _horizontalAxis;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	    _horizontalAxis = Input.GetAxisRaw(transform.parent.name + "_Horizontal");
	}

    void FixedUpdate() {
        if (Dead) return;
        transform.Translate(Vector3.up * Speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward * -_horizontalAxis * RotationSpeed * Time.fixedDeltaTime);
    }


}
