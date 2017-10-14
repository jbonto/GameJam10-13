using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTurn : MonoBehaviour {
	public Transform body;
	private Rigidbody2D PlayerRB;
	private bool facingRight;
	private float direction;
	// Use this for initialization
	void Start () {
		facingRight = true;
		PlayerRB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
			direction = Input.GetAxis ("h");
			if ((direction < 0) && (facingRight == true)) {
				Vector3 theScale = body.transform.localScale;
				theScale.x *= -1;
				body.transform.localScale = theScale;
				facingRight = false;
			}
			if ((direction > 0) && (facingRight == false)) {
				Vector3 theScale = body.transform.localScale;
				theScale.x *= -1;
				body.transform.localScale = theScale;
				facingRight = true;
			}

	}
	
	public bool getTurn(){
		return facingRight;
	}
}
