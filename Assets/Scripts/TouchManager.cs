 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
	[SerializeField]
	GameObject Effect;

	//Animation animation;


	Camera camera;
	Vector2 MousePosition;

    // Start is called before the first frame update
    void Start()
    {
		//animation = gameObject.GetComponent<Animation>();
		camera = GameObject.Find("MainCamera").GetComponent<Camera>();


	}

	// Update is called once per frame
	void Update()
	{
		if(Input.touchCount >0)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase ==TouchPhase.Ended)
			{
				Instantiate(Effect,touch.position , Quaternion.identity);
			}


		}
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("이펙트");
			MousePosition = Input.mousePosition;
			MousePosition = camera.ScreenToWorldPoint(MousePosition);
			Instantiate(Effect, MousePosition, Quaternion.identity);
			//animation.Play();
		}
	}
}
