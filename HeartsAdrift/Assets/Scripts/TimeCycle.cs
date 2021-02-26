using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCycle : MonoBehaviour
{
    public static TimeCycle Instance;
    [SerializeField] Text displayClock;
    [SerializeField] float sunSpeed;

    [HideInInspector] public float hours;
    [HideInInspector] public float minutes;

    Light sun;
    Color dawn;
    Color night;
    Color color1;
    Color color2;
    int clock;
    int day;
    static Quaternion DAWN = Quaternion.Euler(new Vector3(0, 180, 180));
    static Quaternion NOON = Quaternion.Euler(new Vector3(90, 180, 180));
    static Quaternion PREDUSK = Quaternion.Euler(new Vector3(150, 180, 180));
    static Quaternion DUSK = Quaternion.Euler(new Vector3(180, 180, 180));
    static Quaternion MIDNIGHT = Quaternion.Euler(new Vector3(270, 180, 180));
    static Quaternion NEWDAY = Quaternion.Euler(new Vector3(360, 180, 180));
    Quaternion lastRotation;
    Quaternion targetRotation;
    float countdown;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        sun = transform.GetChild(0).GetComponent<Light>();
        dawn[0] = 1;
        dawn[1] = 0.7333333f;
        dawn[2] = 0.07058824f;
        night[0] = 0.4404593f;
        night[1] = 0.8286127f;
        night[2] = 0.9245283f;
        clock = 0;
        day = 0;
        lastRotation = DAWN;
        transform.rotation = DAWN;
        targetRotation = DAWN;
        countdown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime(transform.rotation.eulerAngles.x);

        RotateSun();
        if(targetRotation == DUSK)
        {
            sun.color = Color.Lerp(color1, color2, countdown / (60 / (sunSpeed * .8f)));
        }
        else
            sun.color = Color.Lerp(color1, color2, countdown / (60 / (sunSpeed * 4)));
    }

    private void RotateSun()
    {
        if (transform.rotation == targetRotation)
        {
            if (targetRotation == DAWN || targetRotation == NEWDAY)
            {
                //Debug.Log("DAWN");
                color1 = dawn;
                color2 = Color.white;
                targetRotation = NOON;
                lastRotation = DAWN;
                transform.GetChild(0).gameObject.SetActive(true);
                clock = 0;
                day++;
            }
            else if (targetRotation == NOON)
            {
                //Debug.Log("NOON");
                color1 = Color.white;
                color2 = dawn;
                targetRotation = DUSK;
                lastRotation = NOON;
                clock = 1;
            }
            /*else if (targetRotation == PREDUSK)
            {
                //Debug.Log("DUSK");
                color1 = Color.white;
                color2 = dawn;
                targetRotation = DUSK;
                lastRotation = PREDUSK;
            }*/
            else if (targetRotation == DUSK)
            {
                //Debug.Log("DUSK");
                targetRotation = MIDNIGHT;
                lastRotation = DUSK;
                transform.GetChild(0).gameObject.SetActive(false);
                clock = 2;
            }
            else if (targetRotation == MIDNIGHT)
            {
                //Debug.Log("MIDNIGHT");
                targetRotation = NEWDAY;
                lastRotation = MIDNIGHT;
                clock = 3;
            }
            countdown = 0f;
        }
        
        countdown += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(lastRotation, targetRotation, countdown / (60 / sunSpeed));
    }

    void DisplayTime(float timeToDisplay)
    {
        //Debug.Log(timeToDisplay);
        timeToDisplay %= 360;
        switch (clock)
        {
            case 0:                                  //(06:00 - 12:00) (Angle 0 - 90)
                timeToDisplay += 90f;
                break;
            case 1:
                timeToDisplay = 270f - timeToDisplay; //(12:00 - 18:00) (Angle 90 - 0)
                break;
            case 2:
                timeToDisplay = 630f - timeToDisplay; //(18:00 - 23:59) (Angle 360 - 270)
                break;
            case 3:
                timeToDisplay -= 270f;                //(00:00 - 06:00) (Angle 270 - 360)
                break;
            default:
                break;
        }       

        timeToDisplay *= 4f;

        hours = Mathf.FloorToInt(timeToDisplay / 60);
        minutes = Mathf.FloorToInt(timeToDisplay % 60);

        string time = string.Format("{0:00}:{1:00}", hours, minutes);
        displayClock.text = "Day " + day + ", " + time;
        //Debug.Log(time);
    }

    public float GetHours()
    {
        return hours;
    }

    public float GetMinutes()
    {
        return minutes;
    }
}
