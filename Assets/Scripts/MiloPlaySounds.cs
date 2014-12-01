using UnityEngine;
using System.Collections;

public class MiloPlaySounds : MonoBehaviour
{
    AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
	
    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Plaies the battery pick up.
    /// </summary>
    public void PlayBatteryPickUp()
    {
        source.audio.Play();
    }
}
