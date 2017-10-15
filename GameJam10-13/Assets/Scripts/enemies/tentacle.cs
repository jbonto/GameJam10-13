using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacle : MonoBehaviour {
	public Water bossWater;
	private Animator anim;
	public bool poke;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (poke) {
			anim.SetTrigger ("poke");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDestroy(){
		//bossWater.waterRise ();
	}
	public void onHit(){
		bossWater.waterRise ();
	}
}
