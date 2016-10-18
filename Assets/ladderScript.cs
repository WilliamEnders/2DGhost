using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderScript : MonoBehaviour {

    private bool ladder;
    private Transform player;

    void Update() {
        if (Input.GetButton("B") && ladder) {
            if (!player.GetComponentInParent<generalMovement>().falling) {
                player.GetComponentInParent<Rigidbody2D>().velocity = Vector2.up * 5;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D info) {
        if (info.CompareTag("Player")) {
            ladder = true;
            player = info.transform;
        }
    }

    void OnTriggerExit2D() {
        ladder = false;
    }

}
