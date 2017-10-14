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
	private Rigidbody2D RB2D;

	[Header("Obects")]
	public GameObject firePos, harpoonGO;

	[Header("UI")]
	public Text hpText;
	public Text breathText;
	public Text harpState;

	[Header("Anything Else.")]
	public playerTurn pT;
	public KeyCode jumpKey, fireKey, rLkey;

	// Use this for initialization
	void Start () {
		RB2D = GetComponent<Rigidbody2D> ();
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
		if (canMove) {
			if (isUW) {
				RB2D.velocity = new Vector2 (x * UWMS, RB2D.velocity.y);
				if (Input.GetKeyDown (jumpKey)) {
					RB2D.velocity = new Vector2 (x * UWMS, jump);

				}
			} else {
				RB2D.velocity = new Vector2 (x * moveSpeed, RB2D.velocity.y);

			}

		}

	}

	void harpoonActions(){
		if (Input.GetKeyDown (rLkey) && !harpLoaded && harpoons > 0f) {
			harpoons--;
			harpLoaded = true;
			Debug.Log ("You reloaded your harpoon");
			harpState.text = "You have a harpoon loaded";

		} 
		if (Input.GetKeyDown (fireKey) && harpLoaded) {
			
			GameObject harpFired = Instantiate (harpoonGO, firePos.transform.position, this.transform.rotation);
			if (pT.getTurn ()) {
				harpFired.GetComponent<Harpoon> ().setDir (1f);
			} else {
				harpFired.GetComponent<Harpoon> ().setDir (-1f);
				harpFired.GetComponent<SpriteRenderer> ().flipX = true;
				harpFired.GetComponent<BoxCollider2D> ().offset = new Vector2 (-1.74f, 0f);
				harpFired.GetComponent<BoxCollider2D> ().size = new Vector2 (4.2f, 1.18f);




			}
		
			Debug.Log ("You fired your harpoon");
			harpState.text = "You cannot fire until you reload a harpoon";
			harpLoaded = false;
		}

	}

	public void harpPickUp(){
		harpoons++;
	}
}
