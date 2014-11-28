using UnityEngine;
using System.Collections;

public class MiloShootCannonBall : MonoBehaviour
{
    public GameObject rotatingCanonBallPrefab;
    Animator anim;
    int shootCannonBall;
    GameObject milo;
    GameObject cannonBall;
    bool cooldown = false;
    float angel = 300;
    float MaxAngel = 500;
    float MinAngel = 100;
    Vector2 vMeasures = new Vector2(2.71f, 0.0f);//DON'T MESS WITH THESE NUMBERS!
    //Vector2 vMeasures = new Vector2(1.4f, 0.0f);//DON'T MESS WITH THESE NUMBERS!

    void Awake()
    {
        anim = GetComponent<Animator>();
        shootCannonBall = Animator.StringToHash("ShootCannonBall");
    }

    // Use this for initialization
    void Start()
    {
        milo = GameObject.Find("MiloCannon01");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (StateController.INSTANCE.HasDialoguesBeenStarted)//Ensures we can't attack before both dialogues are done.
        {
            // Checking for scene as well because otherwise, KOS will shoot when Milo shoots.
            if (Input.GetKeyDown(KeyCode.Space) && Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
            {
                if (cooldown == false)
                {
                    cooldown = true;
                    StartCoroutine("ShootingCooldown");
                    StartCoroutine("SpawnCannonball");
                }
            }
            if (Input.GetKey(KeyCode.W) && Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
            {
                increaseAngel();
            }
            if (Input.GetKey(KeyCode.S) && Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
            {        
                decreaseAngel();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "RotatingAxe01")
        {
            if (Application.loadedLevelName.Equals(StateController.nextSceneAsKOS))
            {
                StateController.ConsecutiveHitsValue += 1;
            } else if (Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
            {
                StateController.ConsecutiveHitsValue = 0;
            }
            Destroy(col.gameObject);
        }
    }

    /// <summary>
    /// Increases the angel.
    /// </summary>
    public void increaseAngel()
    {
        if (angel < MaxAngel)
        {
            this.angel += 2; 
        }
    }
    
    /// <summary>
    /// Decreases the angel.
    /// </summary>
    public void decreaseAngel()
    {
        if (angel > MinAngel)
        {
            this.angel -= 2;
        }
    } 

    /// <summary>
    /// Spawns the cannonball.
    /// </summary>
    /// <returns>The cannonball.</returns>
    public IEnumerator SpawnCannonball()
    {
        anim.SetTrigger(shootCannonBall);
        yield return new WaitForSeconds(0.6f);//wait until the animation is done playing, then throw the axe.
        cannonBall = Instantiate(rotatingCanonBallPrefab, new Vector2(milo.transform.position.x - vMeasures.x, milo.transform.position.y - vMeasures.y), Quaternion.identity) as GameObject;
        cannonBall.name = "CannonBall01";
        cannonBall.rigidbody2D.AddForce(Vector2.up * angel + Vector2.right * -800, ForceMode2D.Force);
    }

    /// <summary>
    /// Shootings the cooldown.
    /// </summary>
    /// <returns>The cooldown.</returns>
    public IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        cooldown = false;
    }
}
