using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	Animator anim;
	int jumpHash = Animator.StringToHash("Jump");

	void Start ()
	{
		anim = GetComponent<Animator>();
	}
	
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetTrigger (jumpHash);
		}
	}

}