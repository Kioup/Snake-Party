using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tail : MonoBehaviour {

    public float DistanceBetweenPoints = 0.1f;
    public Transform Head;

    private List<Vector2> _points;
    private EdgeCollider2D _col;
    private LineRenderer _lineRenderer;


    // Use this for initialization
	void Start () {
	    _lineRenderer = GetComponent<LineRenderer>();
	    _col = GetComponent<EdgeCollider2D>();
	    _points = new List<Vector2>();
        AddPoint();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Vector3.Distance(Head.position, new Vector3(_points.Last().x, _points.Last().y, 0)) >= DistanceBetweenPoints)
	        AddPoint();
	}

    void AddPoint() {
        if (_points.Count > 1) _col.points = _points.ToArray();
        _points.Add(Head.position);
        _lineRenderer.numPositions = _points.Count;
        _lineRenderer.SetPosition(_points.Count - 1, Head.position);
    }
}
