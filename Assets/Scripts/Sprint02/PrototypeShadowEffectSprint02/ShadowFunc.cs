using UnityEngine;
using System.Collections;

public class ShadowFunc : MonoBehaviour
{

    private Animator animator;
    private float chaseValue = 1.5f;
    GameObject milo;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        milo = GameObject.Find("MiloSprint02");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "MiloSprint02")
        {
            InvokeRepeating("ChaseMilo", 0f, 0.03f);
            StartCoroutine("ShadowTimer");
        }
    }
	


    // Update is called once per frame
    void Update()
    {
	
    }


    IEnumerator ShadowTimer()
    {
        yield return new WaitForSeconds(10);
        RemoveShadow();
    }

    void RemoveShadow()
    {
        gameObject.SetActive(false);
    }

    void ChaseMilo()
    {
        Vector3 dir = milo.transform.position - transform.position;
        dir.y = 0.0f;
        Vector3 velocity = dir.normalized * chaseValue;
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }
}
