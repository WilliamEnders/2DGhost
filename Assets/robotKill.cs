using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotKill : MonoBehaviour {

    private RaycastHit2D hit;
    private LineRenderer lazer;
    private bool kill;
    private Transform player;
    private bool jobDone;

    // Use this for initialization
    void Start () {

        jobDone = false;
        lazer = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x);

        if (hit) {
            if (hit.transform.CompareTag("Player") && !hit.transform.GetComponent<generalMovement>().dead) {
                Invoke("DoIKill", 0.5f);
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
        lazer.SetPosition(0, transform.position + (Vector3.right * transform.localScale.x) / 1.5f);
        lazer.SetPosition(1, player.position);
    }

    void Dead() {
        lazer.enabled = false;
        kill = false;
        player.GetComponent<generalMovement>().Explode();
    }

}
