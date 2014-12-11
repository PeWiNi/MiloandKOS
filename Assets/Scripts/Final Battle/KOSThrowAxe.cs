using UnityEngine;
using System.Collections;

public class KOSThrowAxe : MonoBehaviour
{
    public GameObject rotatingAxePrefab;
    Animator anim;
    int axeThrow;
    GameObject kos;
    GameObject axe;
    GameObject aimingArm;
    bool cooldown = false;
    bool isShooting = false;
    float angel = 300;
    Vector3 curRot;
    float maxAim = 20.0f;
    float aimSpeed = 15.0f;
    float maxZ;
    float minZ;
    float decreaseMaxZ;
    float decreaseMinZ;
    float MaxAngel = 500;
    float MinAngel = 100;
    Vector2 vMeasures = new Vector2(1.3f, 0.6f);//DON'T MESS WITH THESE NUMBERS!
  
    void Awake()
    {
        anim = GetComponent<Animator>();
        axeThrow = Animator.StringToHash("AxeThrow");
    }

    // Use this for initialization
    void Start()
    {
        kos = GameObject.Find("KOSDoubleAxe01");
        aimingArm = GameObject.Find("KOSAimingArm");
        curRot = aimingArm.transform.eulerAngles;
        maxZ = curRot.z + maxAim;
        minZ = curRot.z - maxAim;
        aimingArm.SetActive(false);
        //  if(kos.transform.position.x > Screen.width, kos.transform.position.x=Screen.width)
    }

    // Update is called once per frame
    void Update()
    {
        if (StateController.INSTANCE.HasDialoguesBeenStarted)//Ensures we can't attack before both dialogues are done.
        {
            if (!Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
            {
                // Checking for scene as well because otherwise, Milo will shoot when Kos shoots.
                if (Input.GetKeyDown(KeyCode.Space) && Application.loadedLevelName.Equals(StateController.nextSceneAsKOS))
                {
                    if (cooldown == false)
                    {
                        cooldown = true;
                        StartCoroutine("ShootingCooldown");
                        StartCoroutine("SpawnAxe");
                        aimingArm.renderer.enabled = false;
                    }
                }
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && !isShooting && Application.loadedLevelName.Equals(StateController.nextSceneAsKOS))
                {
                    aimingArm.SetActive(true);
                    increaseAngel();
                    increaseAimAngel();
                } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && !isShooting && Application.loadedLevelName.Equals(StateController.nextSceneAsKOS))
                {        
                    aimingArm.SetActive(true);
                    decreaseAngel();
                    decreaseAimAngel();
                } else if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    aimingArm.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "CannonBall01")
        {
            if (StateController.ConsecutiveHitsValue != 3)
            {
                if (Application.loadedLevelName.Equals(StateController.nextSceneAsKOS))
                {
                    StateController.ConsecutiveHitsValue = 0;
                    StateController.KOSHitsTakenValue += 1;
                } else if (Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
                {
                    StateController.ConsecutiveHitsValue += 1;
                }
            }
            Destroy(col.gameObject);
        }
    }

    public void increaseAimAngel()
    {
        if (curRot.z < maxZ)
        {
            curRot.z += Input.GetAxis("Vertical") * Time.deltaTime * aimSpeed;
            curRot.z = Mathf.Clamp(curRot.z, minZ, maxZ);
            aimingArm.transform.eulerAngles = curRot;
        }
    }

    public void decreaseAimAngel()
    {
        if (curRot.z > minZ)
        {
            curRot.z -= Input.GetAxis("Vertical") * Time.deltaTime * -aimSpeed;
            curRot.z = Mathf.Clamp(curRot.z, minZ, maxZ);
            aimingArm.transform.eulerAngles = curRot;
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
    /// Spawns the axe.
    /// </summary>
    /// <returns>The axe.</returns>
    public IEnumerator SpawnAxe()
    {	
        anim.SetTrigger(axeThrow);
        yield return new WaitForSeconds(0.6f);//wait until the animation is done playing, then throw the axe.
        axe = Instantiate(rotatingAxePrefab, new Vector2(kos.transform.position.x + vMeasures.x, kos.transform.position.y + vMeasures.y), Quaternion.identity) as GameObject;
        axe.name = "RotatingAxe01";
        axe.rigidbody2D.AddForce(Vector2.up * angel + Vector2.right * 500, ForceMode2D.Force);
        aimingArm.renderer.enabled = true;
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
