using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {

	public Transform player;
	public Vector3 offSet;
	public float speed;
	public Rigidbody2D rb;
    // Update is called once per frame
    void FixedUpdate () {
        if (player) {
            Vector3 newPos = player.position + offSet;
            newPos.y = player.position.y + offSet.y + Mathf.Clamp(rb.velocity.y / 4, -5f, 5f);
            transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
        }
	}
}
