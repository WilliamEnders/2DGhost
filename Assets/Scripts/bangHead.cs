using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bangHead : MonoBehaviour {

	private Rigidbody2D rb;
	private float vel;
	private General gen;

	// Use this for initialization
	void Start () {
		gen = GetComponentInParent<General> ();
		rb = GetComponentInParent<Rigidbody2D> ();
		vel = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(rb.velocity.y < vel){
		vel = rb.velocity.y;
		}
		if(GetComponentInParent<collisionState>().standing){
			vel = 0;
		}
	}

	void OnCollisionEnter2D(Collision2D info){
		if(info.transform.CompareTag("Ground") && !gen.dead){
			if(vel < -30f && gen.falling){
				vel = 0;
				print ("ouch!");
				gen.dead = true;
				Invoke ("Ghost",1f);

			}
		}
	}

	void Ghost(){
		gen.CreateGhost (1);
	}
}
