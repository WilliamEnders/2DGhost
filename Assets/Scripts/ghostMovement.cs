using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMovement : MonoBehaviour {

	public float speed;
	public float lerp;
	private Rigidbody2D rb;
	private CapsuleCollider2D cap;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Camera.main.GetComponent<cameraMove> ().player = transform;
		cap = GetComponent<CapsuleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ghostMove = new Vector3 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
		rb.velocity = Vector3.Lerp (rb.velocity,ghostMove*speed,lerp);
	}

	void OnTriggerStay2D(Collider2D info){
		if(info.CompareTag("Player")){
			cap.enabled = false;
			if(Input.GetButtonDown("B")){
					KillGhost (info.GetComponent<General>());
			}
		}
	}
	void OnTriggerExit2D(Collider2D info){
		if(info.CompareTag("Player")){
			cap.enabled = true;
		}
	}

	void KillGhost(General gen){
		gen.enabled = true;
		Camera.main.GetComponent<cameraMove> ().player = gen.transform;
		gen.dead = false;
		Destroy (transform.gameObject);
	}

}
