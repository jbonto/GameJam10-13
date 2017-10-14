using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angler : MonoBehaviour {
	private BoxCollider2D bodyCol;
	private CapsuleCollider2D eleCol;
	private bool canHurt = true;
	// Use this for initialization
	void Start () {
		BoxCollider2D[] cols = GetComponents<BoxCollider2D> ();
		bodyCol = cols [0];
		eleCol = GetComponent<CapsuleCollider2D> ();
		StartCoroutine (onOff ());
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.transform.GetComponent<Player> () && canHurt) {
				col.transform.GetComponent<Player> ().playerHurt (2, this.transform.position.x);
		}
	}
	public void hitRec(){
		Debug.Log ("hit");
		Destroy (this.gameObject);
	}
	IEnumerator onOff(){
		canHurt = true;
		eleCol.enabled = true;
		yield return new WaitForSeconds (.8f);
		canHurt = false;
		eleCol.enabled = false;
		yield return new WaitForSeconds (.8f);
		StartCoroutine (onOff ());
	}
}
