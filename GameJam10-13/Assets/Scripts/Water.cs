using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		if (col.transform.tag == "Player") {
			col.transform.GetComponent<Player> ().isUW = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.transform.tag == "Player") {
			col.transform.GetComponent<Player> ().isUW = false;
		}
	}
}
