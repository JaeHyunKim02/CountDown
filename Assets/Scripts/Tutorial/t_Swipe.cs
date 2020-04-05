﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_Swipe : MonoBehaviour // 튜토리얼용

{

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    private bool isSwipeing = false;

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = isSwipeing = false;
        if (TutorialGame.instance.turn != TutorialGame.TURN_STATE.STAY)
            return;

        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
            //myStatic.siwpeC -= 1;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }


        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {

                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }

        #endregion

        if (!PauseButton.isPause)
        {
            // Calculate the distance
            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                if (Input.touches.Length > 0)
                {
                    swipeDelta = Input.touches[0].position - startTouch;

                }
                else if (Input.GetMouseButton(0))
                {
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }
            }

            // Did we cross the deadzone?
            if (swipeDelta.magnitude > 125)
            {
                //which direction?
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                isSwipeing = true;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                        swipeLeft = true;
                    else
                        swipeRight = true;
                    //myStatic.siwpeC -= 1;
                }
                else
                {
                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                    //myStatic.siwpeC -= 1;
                }
                CameraShake.instance.ShakingCamera(0.1f, 0.05f);
                Reset();
            }
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool IsSwipeing { get { return isSwipeing; } }


}

