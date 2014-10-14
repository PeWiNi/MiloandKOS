using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour
{
    private int batteryCapacityValue = 25;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Milo")
        {
            Destroy(gameObject);
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().Capacity += batteryCapacityValue;// Add more capacity to the flashlight.
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().CounterLinear = 0;// Resets the counter.
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().light.intensity = 8f;// Max intensity/ fully recharged.
        }
    }
}
