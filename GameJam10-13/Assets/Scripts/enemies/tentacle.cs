using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacle : MonoBehaviour {
	public Water bossWater;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDestroy(){
		bossWater.waterRise ();
	}
	public void onHit(){
		bossWater.waterRise ();
	}
}
