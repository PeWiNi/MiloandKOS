using UnityEngine;
using System.Collections;

public class MiloShootCannonBall : MonoBehaviour
{
    public GameObject rotatingCanonBallPrefab;
    Animator anim;
    int shootCannonBall;
    GameObject milo;
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
        // Checking for scene as well because otherwise, KOS will shoot when Milo shoots.
        if (Input.GetKeyDown(KeyCode.Return) && Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
        {
            StartCoroutine("SpawnCannonball");
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
    /// Spawns the cannonball.
    /// </summary>
    /// <returns>The cannonball.</returns>
    public IEnumerator SpawnCannonball()
    {
        anim.SetTrigger(shootCannonBall);
        yield return new WaitForSeconds(0.6f);//wait until the animation is done playing, then throw the axe.
        GameObject cannonBall = Instantiate(rotatingCanonBallPrefab, new Vector2(milo.transform.position.x - vMeasures.x, milo.transform.position.y - vMeasures.y), Quaternion.identity) as GameObject;
        cannonBall.name = "CannonBall01";
        cannonBall.rigidbody2D.AddForce(Vector2.up * 100 + Vector2.right * -800, ForceMode2D.Force);
    }
}
