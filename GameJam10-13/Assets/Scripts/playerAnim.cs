using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour {
	private Player pl;
	private Animator Anim;
	// Use this for initialization
	void Start () {
		pl = GetComponent<Player> ();
		Anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Anim.SetFloat ("mov", Mathf.Abs(pl.mov));
		Anim.SetBool ("loaded", pl.harpLoaded);
		Anim.SetBool ("melee", pl.isMelee);
	}
}
