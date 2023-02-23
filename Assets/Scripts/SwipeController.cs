using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [NonSerialized] public static float x, y;
    
    private bool touchFinger;
    private Vector2 swipePos;

    void Update()
    {
        if (Input.touchCount > 0) Touch();
    }

    private void Touch()
    {
        x = y = 0;
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchFinger = true;
                break;

            case TouchPhase.Ended:
                touchFinger = false;
                swipePos = Vector2.zero;
                break;

            case TouchPhase.Moved:
                if (touchFinger)
                    swipePos = touch.deltaPosition;
                break;

            case TouchPhase.Stationary:
                if (touchFinger)
                    swipePos = Vector2.zero;
                break;
        }

        x = swipePos.x;
        y = swipePos.y;
    }
}

