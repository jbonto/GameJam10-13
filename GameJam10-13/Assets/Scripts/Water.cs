using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Water : MonoBehaviour {
	public float moveUp, maxY;
	public int moveIterations, moveCount = 0;
	private bool canChange = true;
	public bool boss;
	public GameObject Kraken;
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
		if (canChange && this.transform.position.y < maxY) {
			StartCoroutine (rise ());
		}
	}
	public void waterDrop(){
		if (canChange) {
			StartCoroutine (drop ());
		}
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
		canChange = false;
		for (int x = 0; x < moveCount; x++) {
			for (int i = 0; i < moveIterations; i++) {
				yield return null;
				transform.Translate (0f, (moveUp*-1f), 0f);
			}
		}
		if (boss && (Kraken == null)) {
			SceneManager.LoadScene ("Levelroom");
		}
		canChange = true;
		moveCount = 0;
	}
}
