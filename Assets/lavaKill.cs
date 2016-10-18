using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaKill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D info){
		if(info.transform.CompareTag("Player") || info.transform.CompareTag("Robot")){
			info.GetComponent<generalMovement> ().Explode ();
		}
	}
}
