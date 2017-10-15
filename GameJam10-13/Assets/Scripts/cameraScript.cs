using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {
	public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Player) {
			this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z);
		}
	}
}
