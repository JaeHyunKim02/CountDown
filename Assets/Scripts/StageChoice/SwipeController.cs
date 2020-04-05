using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour
{
    const int A = 0;
    const int B = -800;
    const int C = -1600;
    int Page;
    bool isAnimating;
    GameObject XObj;
    float X;
    void Start()
    {
        Page = 1;
        isAnimating = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        Debug.Log("X is " + X + ", And isAnimating " + isAnimating);

        XObj = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        X = XObj.GetComponent<RectTransform>().localPosition.x;
        if(isAnimating)
        Animating();
    }

    public void OnBeginDrag()
    {
        Debug.Log("Dr");
        isAnimating = false;
    }

    public void OnEndDrag()
    {
        Debug.Log("Dr2");
        isAnimating = true;
    }

    void Animating()
    {
        if (Page != 3)
        {
            if (Mathf.Abs(A - X) < Mathf.Abs(B - X))
            {
                if (-10 < X && X < 1200)
                {
                    XObj.transform.position = new Vector3(0, XObj.transform.position.y, XObj.transform.position.z);
                    Page = 1;
                    isAnimating = false;
                    Debug.Log("isOK" + X);
                }
                else
                    XObj.transform.Translate(0.1f, 0, 0);
            }
            else if (Mathf.Abs(A - X) > Mathf.Abs(B - X))
            {
                if (-1210 < X && X < -1190)
                {
                    XObj.transform.position = new Vector3(-1200, XObj.transform.position.y, XObj.transform.position.z);
                    Page = 2;
                    isAnimating = false;
                    Debug.Log("isOK" + X);
                }
                else
                    XObj.transform.Translate(-0.1f, 0, 0);
            }
        }
        if (Page != 1)
        {
        if (Mathf.Abs(B - X) < Mathf.Abs(C - X))
            {
                if (-1210 < X && X < -1190)
                {
                    XObj.transform.position = new Vector3(-1200, XObj.transform.position.y, XObj.transform.position.z);
                    Page = 2;
                    isAnimating = false;
                    Debug.Log("isOK" + X);
                }
                else
                    XObj.transform.Translate(-0.1f, 0, 0);
            }
            else if (Mathf.Abs(B - X) > Mathf.Abs(C - X))
            {
                if (-3600 < X && X < -2390)
                {
                    XObj.transform.position = new Vector3(-2400, XObj.transform.position.y, XObj.transform.position.z);
                    Page = 3;
                    isAnimating = false;
                    Debug.Log("isOK" + X);
                }
                else
                    XObj.transform.Translate(0.1f, 0, 0);
            }
        }
    }
}
