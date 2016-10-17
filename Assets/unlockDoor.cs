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
        if (info.CompareTag("Player")) {
            if (Input.GetButtonDown("B")) {
                print("unlock!");
                Destroy(transform.gameObject);
            }
        }
    }

}
