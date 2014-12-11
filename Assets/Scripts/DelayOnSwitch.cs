using UnityEngine;
using System.Collections;

public class DelayOnSwitch : MonoBehaviour
{
    public bool checkedPlayerMovement;
    GameObject flashLight;

    // Use this for initialization
    void Start()
    {
        flashLight = GameObject.Find("Flashlight");
    }
	
    // Update is called once per frame
    void Update()
    {
        CheckPlayerState();
    }

    /// <summary>
    /// Disables the count down.
    /// until the player moves the character.
    /// </summary>
    public void DisableCountDown()
    {
        checkedPlayerMovement = false;
        StopCoroutine(flashLight.GetComponent<Flashlight>().CapacityCounter());
    }

    /// <summary>
    /// Checks the state of the player.
    /// </summary>
    void CheckPlayerState()
    {
        if (GameController.INSTANCE.IsPlayingAsMilo && GameController.INSTANCE.Milo.GetComponent<Animator>().GetFloat("Movement") != 0 && !checkedPlayerMovement)
        {
            checkedPlayerMovement = true;
            StartCoroutine(flashLight.GetComponent<Flashlight>().CapacityCounter());
        } else if (GameController.INSTANCE.Kos.activeSelf && GameController.INSTANCE.Kos.GetComponent<Animator>().GetFloat("Movement") != 0 && !checkedPlayerMovement)
        {
            checkedPlayerMovement = true;
            StartCoroutine(GameController.INSTANCE.MiloAwakeCountdown());
        }
    }
}
