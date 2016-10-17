using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bangHead : MonoBehaviour {
  
	private generalMovement gen;

	// Use this for initialization
	void Start () {
		gen = GetComponentInParent<generalMovement> ();
	}

	void OnCollisionEnter2D(Collision2D info){
		if(!gen.dead && gen.deathFall) {
			print ("ouch!");
            gen.dead = true;
            gen.deathFall = false;
            gen.Invoke ("CreateGhost",1f);
		}
	}
}
