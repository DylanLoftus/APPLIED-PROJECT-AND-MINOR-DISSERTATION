using System;
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
    private float datasetStartHour = 9;
    private float rotZeroHour = 12;
    private int totalRotation = -15;
    private MeshRenderer mr;
    private float skyBoxIncrement;
    private float skyBoxInc8Dec;
    private float smallInc;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        mr = GameObject.Find("SkyDome").GetComponent<MeshRenderer>();
    }

    public void SetSunMoonRotation(float timeStamp)
    {
        
        sunMoonStartRotation.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        sunMoonStartRotation.transform.eulerAngles = new Vector3(360f / dayLength * timeStamp + (sunMoonRotationByHour * 2), 0, 0);
        skyBoxIncrement = ((1f / dayLength * timeStamp + (sunMoonRotationByHour * 2)) / 100);
        skyBoxInc8Dec = (float)Math.Round((Decimal)skyBoxIncrement, 8, MidpointRounding.AwayFromZero);
        mr.material.mainTextureOffset = new Vector2(skyBoxInc8Dec, 0);
    }

    public void RotateSunAndMoon(float gameHours)
    {
        
        float startingHour = datasetStartHour - rotZeroHour;
        gameHours += startingHour;
        float rotation = (gameHours % 24) / 24 * 360;

        smallInc = skyBoxIncrement / timeStamp;
        mr.material.mainTextureOffset = new Vector2(rotation / 360, 0);
        transform.rotation = Quaternion.identity;
        transform.RotateAround(Vector3.zero, Vector3.right, rotation);
        transform.LookAt(Vector3.zero);
    }

    private float GetTimeStamp()
    {
        return gameManager.timeStampForSun;
    }
}
