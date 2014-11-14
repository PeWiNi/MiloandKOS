using UnityEngine;
using System.Collections;

public class KOSThrowAxe : MonoBehaviour
{
    public GameObject rotatingAxePrefab;
    Animator anim;
    int axeThrow;
    GameObject kos;
    Vector2 vMeasures = new Vector2(0.6f, 0.3f);//DON'T MESS WITH THESE NUMBERS!

    void Awake()
    {
        anim = GetComponent<Animator>();
        axeThrow = Animator.StringToHash("AxeThrow");
    }

    // Use this for initialization
    void Start()
    {
        kos = GameObject.Find("KOSDoubleAxe01");
    }

    // Update is called once per frame
    void Update()
    {
        // Checking for scene as well because otherwise, Milo will shoot when Kos shoots.
        if (Input.GetKeyDown(KeyCode.Return) && Application.loadedLevelName.Equals("OutroCutsceneKOS"))
        {
            StartCoroutine("SpawnAxe");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "CannonBall01")
        {
            if (Application.loadedLevelName.Equals("OutroCutsceneKOS"))
            {
                StateController.ConsecutiveHitsValue = 0;
            } else if (Application.loadedLevelName.Equals("OutroCutsceneMilo"))
            {
                StateController.ConsecutiveHitsValue += 1;
            }
            Destroy(col.gameObject);
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
        GameObject axe = Instantiate(rotatingAxePrefab, new Vector2(kos.transform.position.x + vMeasures.x, kos.transform.position.y + vMeasures.y), Quaternion.identity) as GameObject;
        axe.name = "RotatingAxe01";
        axe.rigidbody2D.AddForce(Vector2.up * 300 + Vector2.right * 500, ForceMode2D.Force);
    }
}
