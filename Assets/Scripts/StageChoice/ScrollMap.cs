using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScrollMap : MonoBehaviour
{
	float exitTime = 2.3f;
	[SerializeField]
	private Animator animator;

	private void Start()
	{
		StartCoroutine(CheckAnimationState());
	}
	IEnumerator CheckAnimationState()
	{
		while(!animator.GetCurrentAnimatorStateInfo(0).IsName("BackScrollMap"))
		{
			//전환 중ㅇ리 때 실행되는 부분
			yield return null;

		}

		while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < exitTime)
		{
			yield return null;
		}
		//애니메이션 완료 후 실행되는 부분
		Debug.Log("AniStart");
	}
	
}
