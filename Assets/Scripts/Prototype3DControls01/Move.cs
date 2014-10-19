using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float speed = 5f;
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;

	private Animator anim; 
	public int speedFloat;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		speedFloat = Animator.StringToHash("Speed");
	}

	void FixedUpdate()
	{
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
	
		Movement (horizontal, vertical);
	}

	void Movement(float horizontal, float vertical)
	{
		if (horizontal != 0f || vertical != 0f) 
	    {
			Rotating (horizontal, vertical);
			Vector3 movement = new Vector3 (horizontal, 0f, vertical); //Input.GetAxis ("Vertical") * rigidbody.transform.forward + rigidbody.transform.right;
			rigidbody.velocity = movement * speed;
			anim.SetFloat (speedFloat, 5.5f, speedDampTime, Time.deltaTime);
		} 
		else 
		{
			anim.SetFloat(speedFloat, 0f);	
		}
	}

	void Rotating(float horizontal, float vertical)
	{
		Vector3 targetDirection = new Vector3 (horizontal, 0f, vertical);
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp (rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
	    rigidbody.MoveRotation (newRotation);
	}
}
  