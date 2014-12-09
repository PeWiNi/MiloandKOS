using UnityEngine;
using System.Collections;

public class DetectCollisionWithSchoolBenches : MonoBehaviour
{
    bool isCollidingWithBench;
    bool hasMovedOut;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("SchoolBench"))
        {
            isCollidingWithBench = true;
        }
        if (GameController.INSTANCE.Kos.activeSelf && !hasMovedOut)
        {
            hasMovedOut = true;
            GameController.INSTANCE.Kos.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }
    
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag.Equals("SchoolBench"))
        {
            isCollidingWithBench = false;
        }
        if (GameController.INSTANCE.Kos.activeSelf)
        {
            transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
            GameController.INSTANCE.Kos.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
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
