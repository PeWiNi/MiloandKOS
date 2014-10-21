using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float forwardSpeed = 5f;
	public float backwardSpeed = -2.5f;
	public float turnSpeed = 5f;

	private Animator anim; 
	public int speedFloat;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		speedFloat = Animator.StringToHash("Speed");
	}

	void FixedUpdate()
	{
		if (Input.GetKey (KeyCode.W)) 
		{
			ForwardMovement ();
	    } 
		else if (Input.GetKey (KeyCode.S)) 
		{
			BackwardMovement ();
		} 
		else if (Input.GetKeyDown (KeyCode.D)) 
		{
			RotateRight();
		} 
		else if (Input.GetKeyDown (KeyCode.A)) 
		{
			RotateLeft();
		}
		else 
		{
			anim.SetFloat(speedFloat, 0f);	
		}
	}


	void ForwardMovement()
	{
		anim.SetFloat(speedFloat, 2f);
		anim.transform.Translate(0f, 0f, forwardSpeed*Time.deltaTime);		
	}

	void BackwardMovement()
	{
		anim.SetFloat(speedFloat, 2f);
		anim.transform.Translate(0f, 0f, backwardSpeed*Time.deltaTime);	
	}

	void RotateRight()
	{
		anim.transform.Rotate (0, 90, 0);
	}

	void RotateLeft()
	{
		anim.transform.Rotate (0, -90, 0);
	}
}
  