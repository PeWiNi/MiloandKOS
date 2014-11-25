using UnityEngine;
using System.Collections;

public class SwitchFadingInOut : MonoBehaviour
{
    [SerializeField]
    Texture
        black;
    [SerializeField]
    Texture
        white;
    public float fadeSpeed = 1.5f;
    static bool switchStarting = false;
    int switchState = 0;
	int panelWidth;
	int panelHeight;
	bool changed;
        
    void Awake()
    {
		panelWidth = Screen.width;
		panelHeight = Screen.height;
        guiTexture.pixelInset = new Rect(0f, 0f, panelWidth, panelHeight);
    }
        
    void Update()
    {
		if (panelWidth != Screen.width) {
						panelWidth = Screen.width;
						changed = true;
				}
		if (panelHeight != Screen.height) {
						panelHeight = Screen.height;
						changed = true;
				}
		if (changed) {
						guiTexture.pixelInset = new Rect (0f, 0f, panelWidth, panelHeight);
						changed = false;
				}

		if (switchStarting && switchState == 0)
        {
            EndSwitchFade();
        } else if (switchStarting && switchState == 1)
        {
            StartSwitchFade();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="SwitchFadingInOut"/> switch starting.
    /// </summary>
    /// <value><c>true</c> if switch starting; otherwise, <c>false</c>.</value>
    public static bool SwitchStarting
    {
        get
        {
            return switchStarting;
        }
        set
        {
            switchStarting = value;
        }
    }

    /// <summary>
    /// Fades to clear.
    /// </summary>
    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Fades to black.
    /// </summary>
    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        guiTexture.texture = black;
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Fades to white.
    /// </summary>
    void FadeToWhite()
    {
        // Lerp the colour of the texture between itself and white.
        guiTexture.texture = white;
        guiTexture.color = Color.Lerp(guiTexture.color, Color.white, fadeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Starts the switch fade.
    /// </summary>
    void StartSwitchFade()
    {
        // Fade the texture to clear.
        FadeToClear();
        // If the texture is almost clear...
        if (guiTexture.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
            guiTexture.color = Color.clear;
            guiTexture.enabled = false;
            // The switch has ended.
            switchStarting = false;
            switchState = 0;
        }
    }

    /// <summary>
    /// Ends the switch fade.
    /// </summary>
    public void EndSwitchFade()
    {
        // Make sure the texture is enabled.
        guiTexture.enabled = true;

        if (GameController.INSTANCE.IsPlayingAsMilo)
        {
            // Start fading towards black.
            FadeToBlack();
        } else if (!GameController.INSTANCE.IsPlayingAsMilo)
        {
            FadeToWhite();
        }
        // If the screen is almost black...
        if (guiTexture.color.a >= 0.95f)
        {
            /*
             * By calling the SetStateForMiloDragged() method here, we ensure that KOS' settings won't be set, 
             * before the screen has faded completely to black.
             */ 
            GameController.INSTANCE.SetStateForSwitching();
            switchState = 1;
        }
    }
}
