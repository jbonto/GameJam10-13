using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour {
	[Header("Player Stats")]
	public int playerHP;
	public float oxygen;
	public float moveSpeed, UWMS;
	public float jump, UWJ;
	public bool isUW; // isUnderWater
	public float harpoons = 2F;
	private bool harpLoaded = true;
	public bool canMove = true;
	private BoxCollider2D meleeHB;
	private Rigidbody2D RB2D;
	public float jCR;
	public LayerMask layer;
	public bool canJump;

	[Header("Obects")]
	public GameObject firePos, harpoonGO;

	[Header("UI")]
	public Text hpText;
	public Text breathText;
	public Text harpState;

	[Header("Anything Else.")]
	public playerTurn pT;
	public KeyCode jumpKey, fireKey, rLkey, mKey;
	public bool isMelee = false;
	private bool canHurt = true;

	// Use this for initialization
	void Start () {
		BoxCollider2D[] BCS = GetComponents<BoxCollider2D> ();
		RB2D = GetComponent<Rigidbody2D> ();
		meleeHB = BCS [1];
		harpState.text = "You have a harpoon loaded";
		hpText.text = playerHP.ToString ();
		breathText.text = oxygen.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		float hAxis = Input.GetAxis ("h");
		move (hAxis);
		harpoonActions ();
		if (isUW) {
			oxygen -= Time.deltaTime;
			breathText.text = oxygen.ToString ("F0");
		}
	}

	void move(float x){
		canJump = Physics2D.Raycast (this.transform.position, Vector2.down, jCR, layer);
		if (canMove) {
			if (isUW) {
				RB2D.velocity = new Vector2 (x * UWMS, RB2D.velocity.y);
				if (Input.GetKeyDown (jumpKey) && canJump) {
					RB2D.velocity = new Vector2 (x * UWMS, jump);
				}
			} else {
				RB2D.velocity = new Vector2 (x * moveSpeed, RB2D.velocity.y);
				if (Input.GetKeyDown (jumpKey) && canJump) {
					RB2D.velocity = new Vector2 (x * UWMS, (jump*.2f));
				}
			}
		}
	}

	void harpoonActions(){
		if (Input.GetKeyDown (rLkey) && !harpLoaded && harpoons > 0f && !isMelee) {
			harpoons--;
			harpLoaded = true;
			//Debug.Log ("You reloaded your harpoon");
			harpState.text = "You have a harpoon loaded";

		} 
		if (Input.GetKeyDown (fireKey) && harpLoaded && !isMelee) {
			
			GameObject harpFired = Instantiate (harpoonGO, firePos.transform.position, this.transform.rotation);
			if (pT.getTurn ()) {
				harpFired.GetComponent<Harpoon> ().setDir (1f);
			} else {
				harpFired.GetComponent<Harpoon> ().setDir (-1f);
				harpFired.GetComponent<SpriteRenderer> ().flipX = true;
				harpFired.GetComponent<BoxCollider2D> ().offset = new Vector2 (-1.74f, 0f);
				harpFired.GetComponent<BoxCollider2D> ().size = new Vector2 (4.2f, 1.18f);

			}
		
			//Debug.Log ("You fired your harpoon");
			harpState.text = "You cannot fire until you reload a harpoon";
			harpLoaded = false;
		}
		if (Input.GetKeyDown (mKey) && harpLoaded && !isMelee) {
			StartCoroutine (jab ());
		}

	}

	IEnumerator jab(){
		isMelee = true;
		yield return new WaitForSeconds (.35f);
		meleeHB.offset = new Vector2 (2.8f, 1.19f);
		yield return new WaitForSeconds (.65f);
		meleeHB.offset = new Vector2 (0f, 1.19f);
		isMelee = false;
	}

	public void harpPickUp(){
		harpoons++;
	}

	public Rigidbody2D getRB(){
		return RB2D;
	}

	public void getAir(){
		oxygen += .15f;
		breathText.text = oxygen.ToString ("F0");
	}

	public void playerHurt(int x, float ex){
		if (canHurt) {
			//Debug.Log ("ow");
			playerHP -= x;
			hpText.text = playerHP.ToString ();
			StartCoroutine (invincFrames ());
			/**
			if (ex > this.transform.position.x) {
				RB2D.AddForce (new Vector2(0f,3f));
				Debug.Log ("up");
			} else {
				RB2D.AddForce (new Vector2(0f,3f));

				Debug.Log ("down");
			}
			*/
		}

	}

	IEnumerator invincFrames(){
		canHurt = false;
		for (int i = 0; i < 4; i++) {
			this.GetComponent<SpriteRenderer> ().enabled = false;
			yield return new WaitForSeconds (.1f);
			this.GetComponent<SpriteRenderer> ().enabled = true;
			yield return new WaitForSeconds (.1f);
		}
		canHurt = true;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.transform.tag == "Enemy") {
			if (col.GetComponent<Enemy> ()) {
				col.GetComponent<Enemy> ().hit ();
			}
		}
	}
}
