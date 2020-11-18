using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    // Start is called before the first frame update
    Light sun;
    Light moon;
    float duration;
    int timeOfDay;
    Color dawn;
    Color night;
    bool isNight;
    bool isNoon;
    Color color1;
    Color color2;


    void Start()
    {
        sun = transform.GetChild(0).GetComponent<Light>();
        moon = transform.GetChild(1).GetComponent<Light>();
        duration = 0f;
        timeOfDay = 0;
        dawn[0] = 1;
        dawn[1] = 0.7333333f;
        dawn[2] = 0.07058824f;
        night[0] = 0.4404593f;
        night[1] = 0.8286127f;
        night[2] = 0.9245283f;
        isNight = false;
        isNoon = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(180 - transform.rotation.x - Time.time * 8, 90, 0);
        Vector3 angle = transform.eulerAngles;
        
        if (!isNight)
        {
            if (angle.x >= 0 && angle.x < 5f && !isNoon)
            {
                if (timeOfDay == 0)
                {
                    duration = 0f;
                    timeOfDay++;
                    Debug.Log("Dawn");
                }
                color1 = night;
                color2 = dawn;

            }
            else if (angle.x >= 5f && angle.x <= 90f)
            {
                if (timeOfDay == 1 && !isNoon)
                {
                    duration = 0f;
                    timeOfDay++;
                    Debug.Log("Day");
                    isNoon = true;
                }
                color1 = dawn;
                color2 = Color.white;
            }
            else if (angle.x >= 0f && angle.x < 5f && isNoon)
            {
                if (timeOfDay == 2)
                {
                    duration = 0f;
                    timeOfDay++;
                    Debug.Log("Dusk");
                }
                color1 = Color.white;
                color2 = dawn;
            }
            else if (angle.x > 355f && angle.x <= 359f)
            {
                isNight = true;
            }
        }
        

        else if (isNight)
        {
            if (timeOfDay == 3)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                duration = 0f;
                Debug.Log("Night");
                timeOfDay = 0;
            }
            color1 = dawn;
            color2 = night;

            if (angle.x >= 355f && angle.x < 360f && sun.color == night)
            {
                Debug.Log("New Day");
                transform.GetChild(0).gameObject.SetActive(true);
                isNight = false;
                isNoon = false;
                timeOfDay = 0;
            }
        }
        duration += Time.deltaTime / 4;
        sun.color = Color.Lerp(color1, color2, duration);
        transform.localEulerAngles = angle;
    }
}
