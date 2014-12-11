using UnityEngine;
using System.Collections;

public class DetectCollisionWithSchoolBenches : MonoBehaviour
{
    bool isCollidingWithBench;

    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("SchoolBench"))
        {
            isCollidingWithBench = true;
            if (GameController.INSTANCE.Kos.activeSelf)
            {
                GameController.INSTANCE.Kos.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                GameController.INSTANCE.Kos.GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }
    }
    
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag.Equals("SchoolBench"))
        {
            isCollidingWithBench = false;
            if (GameController.INSTANCE.Kos.activeSelf)
            {
                transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
                GameController.INSTANCE.Kos.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                GameController.INSTANCE.Kos.GetComponent<CapsuleCollider>().isTrigger = false;
            }
        }
    }

    /// <summary>
    /// Gets a value indicating is colliding with bench.
    /// </summary>
    /// <value><c>true</c> if is colliding with bench; otherwise, <c>false</c>.</value>
    public bool IsCollidingWithBench
    {
        get
        {
            return isCollidingWithBench;
        }
    }
}
