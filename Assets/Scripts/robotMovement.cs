using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotMovement : MonoBehaviour {
    public Vector2 walkArea;
    public float dir;
    public float speed;
    public bool canMove;
    public bool stationary;

    void Start() {
        canMove = true;
        dir = 1;
        walkArea.x += transform.position.x;
        walkArea.y += transform.position.x;
    }

	// Update is called once per frame
	void FixedUpdate () {

            canMove = !GetComponent<generalMovement>().enabled;

        if (canMove) {
            if (!stationary) {
                if (transform.position.x > walkArea.y) {
                    dir = 1;
                }
                if (transform.position.x < walkArea.x) {
                    dir = -1;
                }

                transform.position += Vector3.left * speed * dir * Time.deltaTime;
                transform.localScale = new Vector3(-dir, 1, 1);
            }
        } else {
            dir = -GetComponent<generalMovement>().move.direction;
            if (Input.GetButtonDown("B")) {
                GetComponent<generalMovement>().CreateGhost();
                canMove = true;
            }
        }


	}


    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + walkArea.x, transform.position.y), 0.5f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + walkArea.y, transform.position.y), 0.5f);
        Gizmos.DrawRay(transform.position,Vector3.right * dir);
    }
}
