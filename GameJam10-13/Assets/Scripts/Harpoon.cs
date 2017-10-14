using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour {
	public bool hasHit = false;
	private Rigidbody2D RB2D;
	public float mSpeed, dir;
	// Use this for initialization
	void Start () {
		RB2D = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!hasHit) {
			RB2D.velocity = new Vector2 (mSpeed * dir, 0f);
		}
	}
	public void setDir(float b){
		dir = b;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Enemy") {
			if (!hasHit) {
				col.transform.GetComponent<Enemy> ().hit ();

			}
		}
		if (col.transform.tag == "Player") {
			if (hasHit) {
				col.transform.GetComponent<Player> ().harpPickUp ();
				Destroy (this.gameObject);
			}
		} else {
			RB2D.gravityScale = 2f;
			hasHit = true;

		}

	}
}
