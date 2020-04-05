using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLight : MonoBehaviour
{
    [SerializeField]
    private Image LightImage;

    private float speed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LightImage.transform.Rotate(0, 0, Time.deltaTime * speed);
    }
}
