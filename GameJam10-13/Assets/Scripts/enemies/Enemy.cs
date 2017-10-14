using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int HP;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(HP <= 0){
			Destroy (this.gameObject);
		}
	}
	public void hit(){
		HP--;
	}
	void OnCollisionEnter2D(Collision2D col){
		//Debug.Log ("col");
		if (col.transform.GetComponent<Player> ()) {
			col.transform.GetComponent<Player> ().playerHurt (1, this.transform.position.x);
		}
	}
}
