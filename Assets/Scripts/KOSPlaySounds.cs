using UnityEngine;
using System.Collections;

public class KOSPlaySounds : MonoBehaviour
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
    /// Plaies the lotus flower pick up.
    /// </summary>
    public void PlayLotusFlowerPickUp()
    {
        source.audio.Play();
    }
}
