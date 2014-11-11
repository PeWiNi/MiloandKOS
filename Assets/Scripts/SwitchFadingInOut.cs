using UnityEngine;
using System.Collections;

public class SwitchFadingInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    static bool switchStarting = false;
        
    void Awake()
    {
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }
        
    void Update()
    {
        if (switchStarting)
            StartSwitchFade();
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
        
    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
        
        
    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }
        
        
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
                
            // The scene is no longer starting.
            switchStarting = false;
        }
    }
        
    public void EndSwitchFade()
    {
        // Make sure the texture is enabled.
        guiTexture.enabled = true;
            
        // Start fading towards black.
        FadeToBlack();
            
        // If the screen is almost black...
        if (guiTexture.color.a >= 0.95f)
        {
            StartSwitchFade();
        }
    }
}
