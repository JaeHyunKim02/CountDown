using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;


    private float CurrentShakingTime;
    private float ShakingTime;
    private float ShakingSize;
    private bool IsShaking;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (IsShaking)
        {
            CurrentShakingTime += Time.deltaTime;
            if (CurrentShakingTime >= ShakingTime)
            {
                CurrentShakingTime = 0;
                ShakingTime = 0;
                ShakingSize = 0;
                IsShaking = false;
            }
            else
            {
                transform.localPosition = (Vector3)Random.insideUnitCircle * ShakingSize + transform.position;
                
            }
        }
        else
        transform.position = new Vector3(0, 0, -18);
    }
    void Update()
    {
    }

    public void ShakingCamera(float time, float shakesize)
    {
        CurrentShakingTime = 0;
        ShakingTime = time;
        ShakingSize = shakesize;
        IsShaking = true;
    }
}