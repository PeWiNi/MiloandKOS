using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    bool on = true;

    private Rect box = new Rect(10, 10, 100, 20);
    private Texture2D background;
    private Texture2D foreground;
    private float capacity = 100;
    private int maxCapacity = 100;
    private int counterLinear = 0;

    // Use this for initialization
    void Start()
    {
        background = new Texture2D(1, 1, TextureFormat.RGB24, false);
        foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);
        
        background.SetPixel(0, 0, Color.black);
        foreground.SetPixel(0, 0, Color.white);
        
        background.Apply();
        foreground.Apply();

        InvokeRepeating("CapacityCounter", 1f, 1f);//Invokes the method every second.
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            on = !on;
        }
        if (on)
        {
            light.enabled = true;
        } else
        {
            light.enabled = false;
        }
        if (capacity < 0)
            capacity = 0;
        if (capacity > maxCapacity)
            capacity = maxCapacity;
    }

    void OnGUI()
    {
        GUI.BeginGroup(box);
        {
            GUI.DrawTexture(new Rect(0, 0, box.width, box.height), background, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(0, 0, box.width * capacity / maxCapacity, box.height), foreground, ScaleMode.StretchToFill);
        }
        GUI.EndGroup();
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
    /// Increases the counterLinear and subtract that amount from the capacity of the flashlight.
    /// </summary>
    void CapacityCounter()
    {
        counterLinear++;
        capacity -= counterLinear;
        if (capacity <= 0)
        {
            light.intensity = 0f;
        } else
        {
            light.intensity -= 0.45f;
        }
    }
}
