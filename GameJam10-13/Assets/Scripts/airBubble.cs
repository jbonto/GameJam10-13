using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airBubble : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col){
		if (col.transform.GetComponent<Player> ()) {
			col.transform.GetComponent<Player> ().getAir ();
		}
	}

}
