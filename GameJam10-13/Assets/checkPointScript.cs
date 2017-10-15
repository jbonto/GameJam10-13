using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointScript : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		if (col.transform.tag == "Player") {
			col.GetComponent<Player> ().getCheckPoint (this.gameObject);
			StartCoroutine (blink ());
		}
	}

	IEnumerator blink(){
		for (int i = 0; i < 4; i++) {
			this.GetComponent<SpriteRenderer> ().enabled = false;
			yield return new WaitForSeconds (.1f);
			this.GetComponent<SpriteRenderer> ().enabled = true;
			yield return new WaitForSeconds (.1f);
		}
	}
}
