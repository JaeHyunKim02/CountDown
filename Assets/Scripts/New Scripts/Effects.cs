using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LifeTime");
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
