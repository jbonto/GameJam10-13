using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour {
	private Vector2 origin;
	private GameObject Player;
	public float range, speed, dist;
	// Use this for initialization
	void Start () {
		origin = this.transform.position;
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		dist = Vector2.Distance (this.transform.position, Player.transform.position);
		if (dist < range) {
			this.transform.position = Vector2.MoveTowards (transform.position, Player.transform.position, speed * Time.deltaTime);
		} else if ((dist >= range)) {
			this.transform.position = Vector2.MoveTowards (transform.position, origin, speed * Time.deltaTime);
		}
	}
}
