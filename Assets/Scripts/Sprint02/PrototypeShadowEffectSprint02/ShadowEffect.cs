using UnityEngine;
using System.Collections;

public class ShadowEffect : MonoBehaviour
{
    Animator anim;
    int movementFloat;
    int jumpingTrigger;
    float verticalMovement = 0.2f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movementFloat = Animator.StringToHash("Movement");
        jumpingTrigger = Animator.StringToHash("Jumping");
    }

    void start()
    {
        Move controller = GameObject.Find("MiloSprint02").GetComponent <Move>();
        //GameObject go = GameObject.Find ("MiloSprint02");
        //Move controller = go.GetComponent <Move> ();
        // float currentSpeed = controller.verticalMovement;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Shadow")
        {
            //InvokeRepeating("ShadowSlowEffect", 3f, 1f);
            ShadowSlowEffect();
            StartCoroutine("speedTime");
            //Destroy(col.gameObject);
        }
    }
    
    IEnumerator speedTime()
    {
        yield return new WaitForSeconds(10);
        revertSpeed();
    }

    void revertSpeed()
    {
        Move.verticalMovement = 1f;
    }

    void ShadowSlowEffect()
    {
        Move.verticalMovement = 0.2f;
       
        //anim.SetFloat(movementFloat, verticalMovement);
        //anim.SetTrigger(jumpingTrigger);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MiloAnim-Run-01"))
        {
            anim.Play("MiloAnim-Walk-01");
        }
    }
}
