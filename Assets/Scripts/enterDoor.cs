using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterDoor : MonoBehaviour {

	public bool door;
    private Transform player;
    private fadeIn fade;
    private dontDestroyInfo info;
    public bool locked;

	// Use this for initialization
	void Start () {
        fade = Camera.main.GetComponent<fadeIn>();
        info = Camera.main.GetComponent<dontDestroyInfo>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("B") && door && !locked){
			print ("you win!");
            player.GetComponentInParent<generalMovement>().move.canMove = false;
            fade.NextLevel();
            info.skipCut = false;
		}
		
	}

	void OnTriggerEnter2D(Collider2D info){
		if(info.CompareTag("Player")){
			door = true;
            player = info.transform;
		}
	}

	void OnTriggerExit2D(){
		door = false;
	}

}
