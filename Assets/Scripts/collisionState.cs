using UnityEngine;
using System.Collections;

public class collisionState : MonoBehaviour {

//	[HideInInspector]
	public LayerMask groundLayer;
//	[HideInInspector]
	public bool standing;
//	[HideInInspector]
	public bool onWall;
//	[HideInInspector]
	public Vector2 bottomPosition = Vector2.zero, rightPosition =Vector2.zero, leftPosition = Vector2.zero;
//	[HideInInspector]
	public float collisionRadius = 10f;
//	[HideInInspector]
	private Color debugCollisionColor = Color.red;
//	[HideInInspector]
	private General general; // Colission State Script
	private Rigidbody2D rb;

	//------------------------------------------------------------------


	void Awake(){

		general = GetComponent<General> ();
		rb = GetComponent<Rigidbody2D> ();

	}

	//------------------------------------------------------------------

	void Start () {}
	void Update () {}

	//------------------------------------------------------------------

	void FixedUpdate(){
	
		var pos = bottomPosition;
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		if (standing) {
			onWall = false;
		}

		// If you're touching the ground layer than standing = true

		standing = Physics2D.OverlapCircle (pos, collisionRadius, groundLayer);

		pos = general.move.x > 0 ? rightPosition : leftPosition;

		pos.x += transform.position.x;
		pos.y += transform.position.y;

		// If you're touching the wall layer than onWall = true

		if (general.move.x != 0) {
			onWall = Physics2D.OverlapCircle (pos, collisionRadius, groundLayer);
		} else {
			onWall = false;
		}

	//------------------------------------------------------------------

	}

	void OnDrawGizmos(){
		Gizmos.color = debugCollisionColor;

		var positions = new Vector2[] { rightPosition, leftPosition, bottomPosition };

		foreach (var position in positions) {
			var pos = position;
			pos.x += transform.position.x;
			pos.y += transform.position.y;

			Gizmos.DrawWireSphere (pos, collisionRadius);
		}
	}
}
