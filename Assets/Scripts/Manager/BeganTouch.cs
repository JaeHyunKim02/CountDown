using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeganTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
        }
#endif

#if UNITY_ANDROID

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GetComponent<AudioSource>().Play();
        }
#endif
    }
}
