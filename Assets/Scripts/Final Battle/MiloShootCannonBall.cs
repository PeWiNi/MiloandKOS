using UnityEngine;
using System.Collections;

public class MiloShootCannonBall : MonoBehaviour
{
    public GameObject rotatingCanonBallPrefab;
    Animator anim;
    int shootCannonBall;
    GameObject milo;
    GameObject cannonBall;
    GameObject aimingArrow;
    bool cooldown = false;
    bool isShooting = false;
    float angel = 300;
    Vector3 curRot;
    float maxAim = 20.0f;
    float aimSpeed = 15.0f;
    float maxZ;
    float minZ;
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
        aimingArrow = GameObject.Find("MiloAimingArrow");
        curRot = aimingArrow.transform.eulerAngles;
        maxZ = curRot.z + maxAim;
        minZ = curRot.z - maxAim;
        aimingArrow.SetActive(false);
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
                    aimingArrow.renderer.enabled = false;
                }
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && !isShooting && Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
            {
                aimingArrow.SetActive(true);
                increaseAngel();
                increaseAimAngel();
            } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && !isShooting && Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
            {    
                aimingArrow.SetActive(true);
                decreaseAngel();
                decreaseAimAngel();
            } else if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                aimingArrow.SetActive(false);
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
                StateController.KOSHitsTakenValue = 0;
            } else if (Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
            {
                StateController.ConsecutiveHitsValue = 0;
                StateController.MiloHitsTakenValue += 1;
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
    /// Increases the aim angel.
    /// </summary>
    public void increaseAimAngel()
    {
        if (curRot.z > minZ)
        {
            curRot.z += -Input.GetAxis("Vertical") * Time.deltaTime * aimSpeed;
            curRot.z = Mathf.Clamp(curRot.z, minZ, maxZ);
            aimingArrow.transform.eulerAngles = curRot;
        }
    }
    
    /// <summary>
    /// Decreases the aim angel.
    /// </summary>
    public void decreaseAimAngel()
    {
        if (curRot.z < maxZ)
        {
            curRot.z -= -Input.GetAxis("Vertical") * Time.deltaTime * -aimSpeed;
            curRot.z = Mathf.Clamp(curRot.z, minZ, maxZ);
            aimingArrow.transform.eulerAngles = curRot;
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
        aimingArrow.renderer.enabled = true;
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
