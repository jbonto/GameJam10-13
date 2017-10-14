using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
	public float moveUp, maxY;
	public int moveIterations, moveCount = 0;

	// Use this for initialization
	void Start(){
		//
		//waterRise ();
	}
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

	public void waterRise(){
		StartCoroutine (rise ());
	}
	public void waterDrop(){
		StartCoroutine (drop ());
	}
	IEnumerator rise(){
		for (int i = 0; i < moveIterations; i++) {
			yield return null;
			transform.Translate (0f, moveUp, 0f);

		}
		moveCount++;
		yield return null;
	}
	IEnumerator drop(){
		for (int x = 0; x < moveCount; x++) {
			for (int i = 0; i < moveIterations; i++) {
				yield return null;
				transform.Translate (0f, (moveUp*-1f), 0f);
			}
		}
	}
}
