using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	public Rigidbody target;
	public float jumpSpeed = 1.0f;
	private const float directionalJumpFactor = 0.7f;
	private const float groundDrag = 5.0f;
	private bool grounded;
	public JumpDelegate onJump = null;
	public LayerMask groundLayers = -1;
	// Which layers should be walkable?
	// NOTICE: Make sure that the target collider is not in any of these layers!
	public float groundedCheckOffset = 0.7f;
	// Tweak so check starts from just within target footing
	private const float groundedDistance = 0.5f;
	// Tweak if character lands too soon or gets stuck "in air" often

	void Setup ()
	{
		if (target == null)
		{
			target = GetComponent<Rigidbody> ();
		}
	}

	void Start ()
		// Verify setup, configure rigidbody
	{
		Setup ();
		// Retry setup if references were cleared post-add
		
		if (target == null)
		{
			Debug.LogError ("No target assigned. Please correct and restart.");
			enabled = false;
			return;
		}
		
		target.freezeRotation = true;
	}

	void FixedUpdate ()
		// Handle movement here since physics will only be calculated in fixed frames anyway
	{
		grounded = Physics.Raycast (
			target.transform.position + target.transform.up * -groundedCheckOffset,
			target.transform.up * -1,
			groundedDistance,
			groundLayers
			);
		// Shoot a ray downward to see if we're touching the ground
		
		if (grounded)
		{
			target.drag = groundDrag;
			// Apply drag when we're grounded
			
			if (Input.GetButton ("Jump"))
				// Handle jumping
			{
				target.AddForce (
					jumpSpeed * target.transform.up +
					target.velocity.normalized * directionalJumpFactor,
					ForceMode.VelocityChange
					);
				// When jumping, we set the velocity upward with our jump speed
				// plus some application of directional movement
				
				if (onJump != null)
				{
					onJump ();
				}
			}
		    }
		     else
		     {
			  target.drag = 0.0f;
			  // If we're airborne, we should have no drag
		     }
	}

}