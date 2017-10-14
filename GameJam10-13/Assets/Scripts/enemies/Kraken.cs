using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour {
	public int health;
	public Water bossWater;
	public GameObject[] tentacles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void hit(){
		health--;
		if (health > 0) {
			bossWater.waterDrop ();

		}
	}
}
