using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterDoor : MonoBehaviour {

	public bool door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("B") && door){
			print ("you win!");
		}
		
	}

	void OnTriggerEnter2D(Collider2D info){
		if(info.CompareTag("Player")){
			door = true;
		}
	}

	void OnTriggerExit2D(){
		door = false;
	}

}
