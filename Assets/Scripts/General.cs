using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {

    //------------------------------------------------------------------
    [HideInInspector]
    public Rigidbody2D rb; // Rigidbody
	[HideInInspector]
	public Vector2 move; // Left Stick

	//------------------------------------------------------------------

	public float speed = 10;
	private float jumpForce = 13;
	private float jumpDelay = .1f;
	public int jumpCount = 2;
	protected float lastJumpTime = 0;
	protected int jumpsRemaining = 0;
	private float slideSpeed = 3;
	private float direction;
	public collisionManager coll;

	[Header("Movement")]
	public bool canMove = true;
	public bool canJump;
	public bool canWallJump;

	private int scale;
	private General gen;

	public bool falling;
	private int reverse;

	public GameObject ghost;

	public bool dead;

    public GameObject ghostPart;

	//------------------------------------------------------------------

	void Start () {
		dead = false;
		rb = GetComponent<Rigidbody2D> ();
		//coll = GetComponent<collisionState> ();

	}

	//------------------------------------------------------------------

	void Update () {
		
		if (rb.velocity.y < -20f && !coll.standing) {
			falling = true;
			rb.freezeRotation = false;
			reverse = rb.velocity.x > 0 ? -1 : 1;
			canMove = false;
		} 

		if (falling && !dead) {
			if (coll.standing) {
				falling = false;
				Invoke ("GetUp",1f);
			} else {
				transform.rotation = Quaternion.Lerp (transform.rotation, new Quaternion (0, 0, reverse, 0), 3f * Time.deltaTime);
			}
		}

		move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if (canMove) {
			Walk ();
			WallSlide();
			if(canJump){
				Jump ();
			}
			if(canWallJump){
				WallJump();
			}
		}
	
	}

	public void GetUp(){
		transform.rotation = Quaternion.identity;
		rb.freezeRotation = true;
		canMove = true;
	}

	//------------------------------------------------------------------

	void Walk(){

        if (move.x > 0.15f) {
				transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
				rb.velocity = new Vector2 (speed * move.x, rb.velocity.y);
			} else if (move.x < -0.15f) {
				transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
				rb.velocity = new Vector2 (speed * move.x, rb.velocity.y);
			} 
		}

	//------------------------------------------------------------------

	void Jump(){

		if (Input.GetButtonDown("Fire1")) {
			if (coll.standing) {
				jumpsRemaining = jumpCount - 1;
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
			} else if(Time.time - lastJumpTime > jumpDelay) {
				if (jumpsRemaining > 0) {
					rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
					jumpsRemaining--;
				}
			}
		}
	}

	//------------------------------------------------------------------

	void WallSlide(){

		if (coll.onWall && coll.standing == false) {
			if (move.x > 0) {
				scale = -1;
			} else if (move.x < 0) {
				scale = 1;
			}
			transform.localScale = new Vector3 (scale, transform.localScale.y, transform.localScale.z);
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}
	}

	//------------------------------------------------------------------

	void WallJump(){
		
		if (Input.GetButtonDown("Fire1") && !coll.standing && coll.onWall) {

			coll.onWall = false;
			canMove = false;
			Invoke ("CanMove", 0.3f);

			if (move.x > 0) {
				direction = -6;
			} else if (move.x < 0) {
				direction = 6;
			}

			if (move.x != 0) {
				rb.velocity = new Vector2 (direction, jumpForce );
			}

			
		}
	}

	void CanMove(){
		canMove = true;
	}

	public void CreateGhost(int type){
		GameObject spook =  Instantiate (ghost,transform.position + Vector3.up,Quaternion.identity) as GameObject;
        GameObject particles = Instantiate(ghostPart,transform.position,transform.rotation) as GameObject;
        particles.GetComponent<particleAttractor>().obj = spook.transform;
		this.enabled = false;
	}

}