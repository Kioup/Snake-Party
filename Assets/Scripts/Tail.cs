using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tail : MonoBehaviour {

    public float DistanceBetweenPoints = 0.1f;
    public float TimeBetweenCreateGap = 5f;
    private Transform _head;

    public float ChanceBetweenGap = 35f;
    public float TimeBetweenGap = 0.5f;
    private bool _canDraw = true;

    private List<Vector2> _points;
    private SnakeController _snakeController;
    private EdgeCollider2D _col;
    private LineRenderer _lineRenderer;

    private List<Vector2> _test;

    // Use this for initialization
    void Start () {
        _head = transform.parent.FindChild("Head").transform;
        _snakeController = transform.parent.Find("Head").GetComponent<SnakeController>();
        _lineRenderer = GetComponent<LineRenderer>();
        _col = GetComponent<EdgeCollider2D>();
        _points = new List<Vector2>();
        ChanceBetweenGap = Random.Range(30, 51);
        AddPoint();
        InvokeRepeating("InvokeCreateGap", TimeBetweenCreateGap, TimeBetweenCreateGap);
    }

    // Update is called once per frame
    void Update () {
        if (_canDraw && Vector3.Distance(_head.position, new Vector3(_points.Last().x, _points.Last().y, 0)) >= DistanceBetweenPoints) {
            AddPoint();
        }
    }


    void AddPoint() {
        if (_points.Count > 1) _col.points = _points.ToArray();
        _points.Add(_head.position);
        _lineRenderer.numPositions = _points.Count;
        _lineRenderer.SetPosition(_points.Count - 1, _head.position);
    }

    private void InvokeCreateGap() {
        Debug.Log("InvokeCreateGap");
        StartCoroutine(CreateGap());
    }

    //TODO: Créer un décalage au niveau des points

    IEnumerator CreateGap() {

        if (enabled && !_snakeController.Dead && Random.Range(0, 101) <= ChanceBetweenGap) {
            Debug.Log("Gap created");
            _canDraw = false;
            yield return new WaitForSeconds(TimeBetweenGap);
            _canDraw = true;
            var tail = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            tail.name = "Tail " + (gameObject.transform.parent.childCount - 1);
            tail.transform.parent = gameObject.transform.parent;
            enabled = false;
        }
    }
}
