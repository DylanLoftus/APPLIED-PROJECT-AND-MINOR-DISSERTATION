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
        StartCoroutine(WaitTime());
        
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        timeStamp = GetTimeStamp();
        SetSunMoonRotation(timeStamp);
    }

    private void SetSunMoonRotation(float timeStamp)
    {
        
        sunMoonStartRotation.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        sunMoonStartRotation.transform.eulerAngles = new Vector3(360f / dayLength * timeStamp + (sunMoonRotationByHour * 2), 0, 0);
        skyBoxIncrement = ((1f / dayLength * timeStamp + (sunMoonRotationByHour * 2)) / 100);
        skyBoxInc8Dec = (float)Math.Round((Decimal)skyBoxIncrement, 8, MidpointRounding.AwayFromZero);
        mr.material.mainTextureOffset = new Vector2(skyBoxInc8Dec, 0);
    }

    public void RotateSunAndMoon()
    {
        smallInc = skyBoxIncrement / timeStamp;
        mr.material.mainTextureOffset += new Vector2(smallInc * 2, 0);
        transform.RotateAround(Vector3.zero, Vector3.right, totalRotation);
        transform.LookAt(Vector3.zero);
    }

    

    private float GetTimeStamp()
    {
        return gameManager.timeStampForSun;
    }
}
