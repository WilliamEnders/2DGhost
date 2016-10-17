using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMovement : MonoBehaviour {

	public float speed;
	public float lerp;
	private Rigidbody2D rb;
    public bool canMove;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Camera.main.GetComponent<cameraMove> ().player = transform;
        canMove = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector3 ghostMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            rb.velocity = Vector3.Lerp(rb.velocity, ghostMove * speed, lerp);
        }
	}

}
