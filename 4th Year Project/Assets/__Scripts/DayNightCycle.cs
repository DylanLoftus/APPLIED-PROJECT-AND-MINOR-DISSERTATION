﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    GameManager gameManager;
    private float timeStamp;
    private float sunMoonRotationByHour = 180 / 12;
    [SerializeField]
    private Transform sunMoonStartRotation;
    private const float dayLength = 24;
    private int totalRotation = -15;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        StartCoroutine(WaitTime());
        
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("In waittime");
        timeStamp = GetTimeStamp();
        SetSunMoonRotation(timeStamp);
    }

    private void SetSunMoonRotation(float timeStamp)
    {
        sunMoonStartRotation.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        sunMoonStartRotation.transform.eulerAngles = new Vector3(360f / dayLength * timeStamp + (sunMoonRotationByHour * 2), 0, 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void RotateSunAndMoon()
    {
        Debug.Log(totalRotation);
        transform.RotateAround(Vector3.zero, Vector3.right, totalRotation);
        transform.LookAt(Vector3.zero);
    }

    private float GetTimeStamp()
    {
        Debug.Log("Timestamp" +gameManager.timeStampForSun);
        return gameManager.timeStampForSun;
    }
}
