using UnityEngine;
using System.Collections;

public class collisionManager : MonoBehaviour {

	public LayerMask groundLayer;
	public bool standing;
	public bool onWall;

    private Vector2 pos;
    private CapsuleCollider2D cap;

	//------------------------------------------------------------------


	void Awake(){
      
        cap = GetComponent<CapsuleCollider2D>();
	}

	//------------------------------------------------------------------
    

	//------------------------------------------------------------------

	void FixedUpdate(){
        Vector2 capPos = new Vector2(transform.position.x, transform.position.y) + cap.offset;
        Vector2 capSize = new Vector2(1f,2f);

        standing = Physics2D.OverlapCapsule(capPos, cap.size, cap.direction,0f,groundLayer);

        if (!standing) {
            onWall = Physics2D.OverlapCapsule(capPos, capSize, cap.direction, 0f, groundLayer);
        } else {
            onWall = false;
        }
    }
}
