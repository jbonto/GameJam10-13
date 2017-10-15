using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour {
	private GameObject Fish;
	public float speed;
	public Transform[] points;
	private Transform currentPoint;
	private int pointSelection = 0;

	// Use this for initialization
	void Start () {
		Fish = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		currentPoint = points [pointSelection];
		Fish.transform.position = Vector3.MoveTowards (Fish.transform.position, currentPoint.position,
			speed * Time.deltaTime);
		if (Fish.transform.position == currentPoint.position) {
			pointSelection++;
			if (pointSelection == points.Length) {
				pointSelection = 0;
			}
		}
	}
}
