using UnityEngine;
using System.Collections;

public class DelayOnSwitch : MonoBehaviour
{
    bool checkedPlayerMovement;
    GameObject flashLight;

    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
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
        if (GameController.INSTANCE.IsPlayingAsMilo)
        {
            Debug.Log("LOLOLA");
            StopCoroutine(flashLight.GetComponent<Flashlight>().CapacityCounter());
        } else if (!GameController.INSTANCE.IsPlayingAsMilo)
        {
            GameController.INSTANCE.ResetMiloAwakeTimer();
        }
    }

    /// <summary>
    /// Checks the state of the player.
    /// </summary>
    void CheckPlayerState()
    {
        if (GameController.INSTANCE.IsPlayingAsMilo && GameController.INSTANCE.Milo.GetComponent<Animator>().GetFloat("Movement") != 0 && !checkedPlayerMovement)
        {
            Debug.Log("Was here");
            checkedPlayerMovement = true;
            StartCoroutine(flashLight.GetComponent<Flashlight>().CapacityCounter());
        } else if (!GameController.INSTANCE.IsPlayingAsMilo && GameController.INSTANCE.Kos.GetComponent<Animator>().GetFloat("Movement") != 0 && !checkedPlayerMovement)
        {
            Debug.Log("Nah wasn't here");
            checkedPlayerMovement = true;
            StartCoroutine(GameController.INSTANCE.MiloAwakeCountdown());
        }
    }
}
