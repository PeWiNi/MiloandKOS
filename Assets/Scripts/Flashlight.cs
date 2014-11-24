using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
//    private bool on = true;

    private Rect box = new Rect(10, 10, 100, 20);
    private Texture2D background;
    private Texture2D foreground;
    private float capacity = 15.0F;
    private int maxCapacity = 15;
    private int counterLinear = 0;
    bool pauseCapacity;

    // Use this for initialization
    void Start()
    {
        background = new Texture2D(1, 1, TextureFormat.RGB24, false);
        foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);
        
        background.SetPixel(0, 0, Color.blue);
        foreground.SetPixel(0, 0, Color.yellow);
        
        background.Apply();
        foreground.Apply();

        InvokeRepeating("CapacityCounter", 1f, 1f);//Invokes the method every second.
    }
	
    // Update is called once per frame
    void Update()
    {
        if (capacity < 0)
        {
            capacity = 0;
        }
        if (capacity > maxCapacity)
        {
            capacity = maxCapacity;
        }
    }

    void OnGUI()
    {
        if (GameController.INSTANCE.IsPlayingAsMilo)
        {
            GUI.BeginGroup(box);
            {
                GUI.DrawTexture(new Rect(0, 0, box.width, box.height), background, ScaleMode.StretchToFill);
                GUI.DrawTexture(new Rect(0, 0, box.width * capacity / maxCapacity, box.height), foreground, ScaleMode.StretchToFill);
            }
            GUI.EndGroup();
        }
    }

    /// <summary>
    /// Gets or sets the capacity.
    /// </summary>
    /// <value>The capacity.</value>
    public float Capacity
    {
        get
        {
            return capacity;
        }
        set
        {
            capacity = value;
        }
    }

    /// <summary>
    /// Gets or sets the max capacity.
    /// </summary>
    /// <value>The max capacity.</value>
    public int MaxCapacity
    {
        get
        {
            return maxCapacity;
        }
        set
        {
            maxCapacity = value;
        }
    }

    /// <summary>
    /// Gets or sets the counter linear.
    /// </summary>
    /// <value>The counter linear.</value>
    public int CounterLinear
    {
        get
        {
            return counterLinear;
        }
        set
        {
            counterLinear = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Flashlight"/> pause capacity.
    /// </summary>
    /// <value><c>true</c> if pause capacity; otherwise, <c>false</c>.</value>
    public bool PauseCapacity
    {
        get
        {
            return pauseCapacity;
        }
        set
        {
            pauseCapacity = value;
        }
    }

    /// <summary>
    /// Increases the counterLinear and subtract that amount from the capacity of the flashlight.
    /// </summary>
    void CapacityCounter()
    {
        if (!pauseCapacity)
        {
            capacity -= 1.0f;
            if (capacity <= 0)
            {
                light.intensity = 0f;
            } else
            {
                light.intensity -= 0.27f;
            }
        }
    }

    /// <summary>
    /// Resets all values.
    /// </summary>
    public void ResetValues()
    {
        capacity = maxCapacity;
        counterLinear = 0;
        light.intensity = 8;
    }
}
