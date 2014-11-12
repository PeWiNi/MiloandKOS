using UnityEngine;
using System.Collections;

public class KOSThrowAxe : MonoBehaviour
{
    Animator anim;
    int axeThrow;
    int notThrowing;
    GameObject axe;

    void Awake()
    {
        anim = GetComponent<Animator>();
        axeThrow = Animator.StringToHash("AxeThrow");
        notThrowing = Animator.StringToHash("NotThrowing");
    }
    // Use this for initialization
    void Start()
    {
        axe = GameObject.Find("RotatingAxe01");
        axe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C)) //&& anim.GetBool(notThrowing))
        {
            anim.SetTrigger(axeThrow);
            anim.SetBool(notThrowing, true);
            axe.SetActive(true);
            axe.rigidbody2D.AddForce(Vector2.up * 40 + Vector2.right * 100, ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "CannonBall01")
        {
            Destroy(col.gameObject);
        }
    }
}
