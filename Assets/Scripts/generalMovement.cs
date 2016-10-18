using UnityEngine;
using System.Collections;

public class generalMovement : MonoBehaviour {

    //------------------------------------------------------------------
    [HideInInspector]
    public Rigidbody2D rb; // Rigidbody
    [HideInInspector]
    public Vector2 leftStick; // Left Stick

    //------------------------------------------------------------------

    [System.Serializable]
    public class Movement {
        [Header("Movement")]
        public bool canMove = true;
        public float speed = 10;
        [Header("Jump")]
        public bool canJump;
        public int jumpCount = 2;
        public float jumpForce = 13;
        [HideInInspector]
        public float jumpDelay = .1f;
        [HideInInspector]
        public float lastJumpTime = 0;
        [HideInInspector]
        public int jumpsRemaining = 0;
        [Header("Wall Jump")]
        public bool canWallJump;
        public float slideSpeed = 3;
        public int direction;
    }
    [System.Serializable]
    public class Collision {
        public LayerMask groundLayer;
        public bool standing;
        public bool onWall;
        [HideInInspector]
        public CapsuleCollider2D cap;
    }

    [System.Serializable]
    public class Ghost {
        public GameObject ghostPre;
        public GameObject ghostPart;
    }


    public Movement move;
    public Ghost ghost;
    public Collision coll;

    public bool falling;
    public bool deathFall;
    public bool dead;
	public bool reallyDead;
    public bool gameOver;

    private int reverse;

    //------------------------------------------------------------------

    void Start() {
        coll.standing = true;
		reallyDead = false;
        move.direction = 1;
        dead = false;
        rb = GetComponent<Rigidbody2D>();
        coll.cap = GetComponent<CapsuleCollider2D>();
    }

    void Update() {

        if (leftStick.x > 0) {
            move.direction = 1;
        } else if(leftStick.x < 0) {
            move.direction = -1;
        }

        transform.localScale = new Vector3(move.direction,1,1);

        leftStick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        CheckCollision();
        
        falling = rb.velocity.y < -20f;

        if (rb.velocity.y < -30f) {
            deathFall = true;
        }

        if (falling) {
            move.canMove = false;
            rb.freezeRotation = false;
            reverse = rb.velocity.x > 0 ? -1 : 1;
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, reverse, 0), 3f * Time.deltaTime);
        }

        if (falling && coll.standing) {
            Invoke("GetUp",1.5f);
        }

        if (move.canMove) {
            Walk();
            Jump();
        }

        if (coll.onWall && coll.standing == false) {
            rb.velocity = new Vector2(0, -move.slideSpeed);
        }

    }


    void Walk() {
        if (leftStick.x > 0.15f) {
            rb.velocity = new Vector2(move.speed * leftStick.x, rb.velocity.y);
        } else if (leftStick.x < -0.15f) {
            rb.velocity = new Vector2(move.speed * leftStick.x, rb.velocity.y);
        }
    }

    void Jump() {

        if (Input.GetButtonDown("Fire1")) {
            if (coll.standing) {
                move.jumpsRemaining = move.jumpCount - 1;
                rb.velocity = new Vector2(rb.velocity.x, move.jumpForce);
            } else if (Time.time - move.lastJumpTime > move.jumpDelay) {
                if (move.jumpsRemaining > 0) {
                    rb.velocity = new Vector2(rb.velocity.x, move.jumpForce);
                    move.jumpsRemaining--;
                }
            }
        }
    }

    public void GetUp() {
        if (!dead) { 
        transform.rotation = Quaternion.identity;
        rb.freezeRotation = true;
            move.canMove = true;
        }
    }

    void CheckCollision() {
        Vector2 capPos = new Vector2(transform.position.x, transform.position.y) + coll.cap.offset;
        Vector2 capSize = new Vector2(1.1f, 2.1f);

        coll.standing = Physics2D.OverlapCapsule(capPos, coll.cap.size, coll.cap.direction, 0f, coll.groundLayer);

        if (!coll.standing) {
            coll.onWall = Physics2D.OverlapCapsule(capPos, capSize, coll.cap.direction, 0f, coll.groundLayer);
        } else {
            coll.onWall = false;
        }

    }

    public void CreateGhost() {
        GameObject spook = Instantiate(ghost.ghostPre, transform.position + Vector3.up, Quaternion.identity) as GameObject;
        GameObject particles = Instantiate(ghost.ghostPart, transform.position, transform.rotation) as GameObject;
        particles.transform.parent = transform;
        particles.GetComponent<particleAttractor>().obj = spook.transform;
        enabled = false;
    }

    public void Explode() {
		if(!reallyDead){
		reallyDead = true;
        gameOver = true;
        move.canMove = false;
        Camera.main.GetComponent<fadeIn>().Invoke("Reload",1f);
		}
    }
}