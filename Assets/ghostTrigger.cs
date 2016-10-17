using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostTrigger : MonoBehaviour {

    private Rigidbody2D rb;
    public GameObject particle;
    private bool canEnter;
    private generalMovement infoGen;

    void Start() {
        canEnter = false;
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetButtonDown("B") && canEnter) {
            KillGhost(infoGen);
        }
    }

    void OnTriggerStay2D(Collider2D info) {
        if (info.CompareTag("Player")) {
            canEnter = true;
            infoGen = info.GetComponentInParent<generalMovement>();
        }
    }

    void OnTriggerExit2D() {
        if (canEnter) {
            canEnter = false;
        }
    }


    void KillGhost(generalMovement gen) {
        rb.velocity = Vector3.zero;
        GetComponentInParent<ghostMovement>().canMove = false;
        GameObject ghostPart = Instantiate(particle, transform.position, transform.rotation) as GameObject;
        ghostPart.GetComponent<particleAttractor>().obj = gen.transform;
        gen.enabled = true;
        gen.dead = false;
        gen.Invoke("GetUp",2f);
        Camera.main.GetComponent<cameraMove>().player = gen.transform;
        Destroy(transform.parent.gameObject, 2f);
    }

}
