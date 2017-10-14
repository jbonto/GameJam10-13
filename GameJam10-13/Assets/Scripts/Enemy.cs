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
}
