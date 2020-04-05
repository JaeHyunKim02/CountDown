using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEnd : MonoBehaviour
{
	Animator animator;
	[SerializeField]
	Animation animation;
    // Start is called before the first frame update
    void Start()
    {
		animator =  gameObject.GetComponent<Animator>();
		animation = gameObject.GetComponent<Animation>();


	}

    // Update is called once per frame
    void Update()
    {
  //      if(!animation.isPlaying)
		//{
		//	Destroy(gameObject);
		//}
    }
	public void Test()
	{
			Destroy(gameObject);
	
	}
}
