using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public void Awake()
    {
        if (instance != null)//싱글톤
            Destroy(gameObject);

        instance = this;
        
    }

    public IEnumerator BigAndSmall(GameObject obj)
    {
        for (int i = 50; i <= 100; i++)
        {
            obj.transform.localScale = new Vector3(i, i, 0f);
            yield return new WaitForFixedUpdate();
        }
        for (int i = 100; i <= 50; i--)
        {
            obj.transform.localScale = new Vector3(i, i , 0f);
            yield return new WaitForFixedUpdate();
        }
        
    }

	
}
