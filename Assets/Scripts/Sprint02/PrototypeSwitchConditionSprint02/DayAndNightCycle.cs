using UnityEngine;
using System.Collections;

public class DayAndNightCycle : MonoBehaviour
{
    public Transform sun;
    public float dayCycleInMinutes = 1.0f;
    const float _SECOND = 1.0f;
    const float _MINUTE = 60.0f * _SECOND;
    const float _HOUR = 60.0f * _MINUTE;
    const float _DAY = 24.0f * _HOUR;
    const float _DEGREES_PER_SECOND = 360 / _DAY;
    float degreeRotation;
    float timeOfDay;

    // Use this for initialization
    void Start()
    {
        timeOfDay = 0.0f;
        degreeRotation = _DEGREES_PER_SECOND * _DAY / (dayCycleInMinutes * _MINUTE);
    }
	
    // Update is called once per frame
    void Update()
    {
        sun.Rotate(new Vector3(degreeRotation, 0.0f, 0.0f) * Time.deltaTime);
        timeOfDay += Time.deltaTime;
        Debug.Log(timeOfDay);
    }
}
