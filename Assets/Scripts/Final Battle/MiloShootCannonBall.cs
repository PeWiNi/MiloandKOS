using UnityEngine;
using System.Collections;

public class MiloShootCannonBall : MonoBehaviour
{
    Animator anim;
    int shootCannonBall;
    GameObject cannonBall;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        shootCannonBall = Animator.StringToHash("ShootCannonBall");
    }
    // Use this for initialization
    void Start()
    {
        cannonBall = GameObject.Find("CannonBall01");
        cannonBall.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C)) //&& anim.GetBool(notThrowing))
        {
            anim.SetTrigger(shootCannonBall);
            cannonBall.SetActive(true);
            cannonBall.rigidbody2D.AddForce(Vector2.up * 35 + Vector2.right * -100, ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "RotatingAxe01")
        {
            Destroy(col.gameObject);
        }
    }
}
