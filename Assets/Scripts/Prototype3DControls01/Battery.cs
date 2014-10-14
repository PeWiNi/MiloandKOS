using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour
{
    private int batteryCapacityValue;

    // Use this for initialization
    void Start()
    {
        batteryCapacityValue = GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().MaxCapacity;
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
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().Capacity = batteryCapacityValue;// Add max capacity to the flashlight.
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().CounterLinear = 0;// Resets the counter.
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().light.intensity = 8f;// Max intensity/ fully recharged.
        }
    }
}
