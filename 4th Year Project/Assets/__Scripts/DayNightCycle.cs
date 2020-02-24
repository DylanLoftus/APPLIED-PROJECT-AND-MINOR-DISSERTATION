using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    GameManager gameManager;
    private float timeStamp;
    private float timeStampInSeconds;
    private float sunMoonRotation;
    [SerializeField]
    private Transform sunMoonStartRotation;
    private const float dayLength = 24;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        timeStamp = GetTimeStamp();

        if(timeStamp > 0)
        {
            timeStampInSeconds = timeStamp * 60 * 60;
            sunMoonStartRotation.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            sunMoonStartRotation.transform.eulerAngles = new Vector3(360f / timeStamp, 0, 0);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeStamp == 0)
        {
            Start();
        }

        if(timeStampInSeconds > 0)
        {
            RotateSunAndMoon();
        }
    }

    private void RotateSunAndMoon()
    {
        timeStamp = GetTimeStamp();

        transform.Rotate(360f * timeStampInSeconds / 2, 0, 0);

    }

    private float GetTimeStamp()
    {
        Debug.Log("Timestamp" +gameManager.timeStampForSun);
        return gameManager.timeStampForSun;
    }
}
