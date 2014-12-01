using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour
{
    int batteryCapacityValue;
    MiloPlaySounds miloSounds;

    // Use this for initialization
    void Start()
    {
        batteryCapacityValue = GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().MaxCapacity;
        miloSounds = GameController.INSTANCE.Milo.GetComponent<MiloPlaySounds>();
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    /// <summary>
    /// Raises the trigger enter event.
    /// </summary>
    /// <param name="col">Col.</param>
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Milo")
        {
//            Destroy(gameObject);
            miloSounds.PlayBatteryPickUp();
            gameObject.SetActive(false);
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().Capacity = batteryCapacityValue;// Add max capacity to the flashlight.
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().CounterLinear = 0;// Resets the counter.
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().light.intensity = 8f;// Max intensity/ fully recharged.
        }
    }
}
