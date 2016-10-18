using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotMovement : MonoBehaviour {
    public Vector2 walkArea;
    public float dir;
    public float speed;
    public bool canMove;
    private RaycastHit2D hit;
    private LineRenderer lazer;
    private bool kill;
    private Transform player;
    private bool jobDone;

    void Start() {
        jobDone = false;
        lazer = GetComponent<LineRenderer>();
        canMove = true;
        dir = 1;
        walkArea.x += transform.position.x;
        walkArea.y += transform.position.x;
    }

	// Update is called once per frame
	void FixedUpdate () {

        canMove = !GetComponent<generalMovement>().enabled;

        if (canMove) {

            if (transform.position.x > walkArea.y) {
                dir = -1;
            }
            if (transform.position.x < walkArea.x) {
                dir = 1;
            }

            transform.position += Vector3.right * speed * dir * Time.deltaTime;

            hit = Physics2D.Raycast(transform.position, Vector2.right * dir);

            if (hit) {
                if (hit.transform.CompareTag("Player") && !hit.transform.GetComponent<generalMovement>().dead) {
                    Invoke("DoIKill",0.5f);
                }
            }
        } else {
            dir = GetComponent<generalMovement>().move.direction;
            if (Input.GetButtonDown("B")) {
                GetComponent<generalMovement>().CreateGhost();
                canMove = true;
            }
        }

        if (kill) {
            KillLazer(player);
        }

	}

    void DoIKill() {
        if (hit) {
			if (hit.transform.CompareTag("Player") && !jobDone && !hit.transform.GetComponent<generalMovement>().dead) {
                    Invoke("Dead", 2f);
                    kill = true;
                    jobDone = true;
                    player = hit.transform;
            }
        }
    }

    void KillLazer(Transform player) {
        lazer.enabled = true;
        lazer.SetPosition(0,transform.position + Vector3.up);
        lazer.SetPosition(1, player.position);
    }

    void Dead() {
            lazer.enabled = false;
            kill = false;
            player.GetComponent<generalMovement>().Explode();
    }
    

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + walkArea.x, transform.position.y), 0.5f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + walkArea.y, transform.position.y), 0.5f);
        Gizmos.DrawRay(transform.position,Vector3.right * dir);
    }
}
