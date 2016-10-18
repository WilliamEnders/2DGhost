using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D info) {
        if (info.CompareTag("Player") || info.CompareTag("Robot")) {
            print("unlock!");
            enterDoor door = GameObject.Find("Door").GetComponent<enterDoor>();
            if (door.locked) {
                door.locked = false;
                door.GetComponent<Animator>().SetTrigger("OpenDoor");
            }
            Destroy(transform.gameObject);
        }
    }

}
