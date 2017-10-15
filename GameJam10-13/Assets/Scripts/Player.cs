using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {
	[Header("Player Stats")]
	public int playerHP;
	public float oxygen, maxO2;
	public float moveSpeed, UWMS;
	public float jump, UWJ;
	public bool isUW; // isUnderWater
	public float harpoons = 2F;
	public bool harpLoaded = true;
	public bool canMove = true;
	//private BoxCollider2D meleeHB;
	private CircleCollider2D meleeHB;
	private Rigidbody2D RB2D;
	public float jCR;
	public LayerMask layer, eLayer;
	public bool canJump;
	public float mov;

	[Header("Obects")]
	public GameObject firePos, harpoonGO;
	public GameObject checkPoint;

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
		maxO2 = oxygen;
		RB2D = GetComponent<Rigidbody2D> ();
		meleeHB = GetComponent<CircleCollider2D> ();
		harpState.text = "You have a harpoon loaded.  You have "+harpoons.ToString()+" spare harpoons";
		hpText.text = playerHP.ToString ();
		breathText.text = oxygen.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		/**
		 * 
		 * 
		 * */
		float hAxis = Input.GetAxis ("h");
		mov = hAxis;
		move (hAxis);
		harpoonActions ();
		if (isUW) {
			oxygen -= Time.deltaTime;
			breathText.text = oxygen.ToString ("F0");
		} else {
			if (oxygen < maxO2) {
				oxygen += (Time.deltaTime*3f);
				breathText.text = oxygen.ToString ("F0");
			}
		}
		if((oxygen <= 0f)||(playerHP <=0)){
			if (checkPoint) {
				goToCheckpoint ();
			} else {
				SceneManager.LoadScene ("Bossroom");	
			}
		}

	}

	void move(float x){
		if (Physics2D.Raycast (this.transform.position, Vector2.down, jCR, layer) ||
		    Physics2D.Raycast (this.transform.position, Vector2.down, jCR, eLayer)) {
			canJump = true;
		} else {
			canJump = false;
		}
		if (canMove) {
			if (isUW) {
				RB2D.velocity = new Vector2 (x * UWMS, RB2D.velocity.y);
				if (Input.GetKeyDown (jumpKey) && canJump) {
					RB2D.velocity = new Vector2 (x * UWMS, jump);
				}
			} else {
				RB2D.velocity = new Vector2 (x * moveSpeed, RB2D.velocity.y);
				if (Input.GetKeyDown (jumpKey) && canJump) {
					RB2D.velocity = new Vector2 (x * UWMS, (jump*.65f));
				}
			}
		}
	}

	public void getCheckPoint(GameObject c){
		checkPoint = c;
	}

	void goToCheckpoint(){
		playerHP = 6;
		hpText.text = playerHP.ToString ();
		harpoons = 2f;
		harpState.text = "You have a harpoon loaded.  You have "+harpoons.ToString()+" spare harpoons";
		harpLoaded = true;
		oxygen = maxO2;
		breathText.text = oxygen.ToString ("F0");
		this.transform.position = new Vector3 (checkPoint.transform.position.x, checkPoint.transform.position.y, this.transform.position.z);
		GameObject[] harps = GameObject.FindGameObjectsWithTag ("Harpoon");
		if (harps.Length > 0) {
			for (int i = 0; i < harps.Length; i++) {
				Destroy (harps [i].gameObject);
			}
		}
	}

	void harpoonActions(){
		if (Input.GetKeyDown (rLkey) && !harpLoaded && harpoons > 0f && !isMelee) {
			harpoons--;
			harpLoaded = true;
			//Debug.Log ("You reloaded your harpoon");
			harpState.text = "You have a harpoon loaded.  You have "+harpoons.ToString()+" spare harpoons";

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
		meleeHB.offset = new Vector2 (.56f, .17f);
		yield return new WaitForSeconds (.65f);
		meleeHB.offset = new Vector2 (0f, .17f);
		isMelee = false;
	}

	public void harpPickUp(){
		harpoons++;
		if (harpLoaded) {
			harpState.text = "You have a harpoon loaded.  You have "+harpoons.ToString()+" spare harpoons";

		}
	}

	public Rigidbody2D getRB(){
		return RB2D;
	}

	public void getAir(){
		if (oxygen < maxO2) {
			oxygen += .15f;
		}
		breathText.text = oxygen.ToString ("F0");
	}

	public void playerHurt(int x, float ex){
		//Debug.Log (ex);
		if (canHurt) {
			//Debug.Log ("ow");
			playerHP -= x;
			hpText.text = playerHP.ToString ();
			StartCoroutine (invincFrames ());
			/*
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
		if (isMelee) {
			if (col.transform.tag == "Enemy") {
				if (col.GetComponent<Enemy> ()) {
					col.GetComponent<Enemy> ().hit ();
				}
				if (col.GetComponent<Kraken> ()) {
					col.GetComponent<Kraken> ().hit ();
				}
			}
		}
	}
}
