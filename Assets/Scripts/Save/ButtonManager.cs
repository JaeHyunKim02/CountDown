using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
	[SerializeField]
	private Sprite NONE, ONE, TWO, THREE, NEXT;

	private void Start()
	{
        if(!PlayerPrefs.HasKey("StageLevel_1"))//완전 처음
            PlayerPrefs.SetInt("StageLevel_1", 4);//맨 첫스테이지 열어줌
       // PlayerPrefs.Save();//Save를 이때쓰는게 맞는지 모르지만 세이브

        switch (PlayerPrefs.GetInt("StageLevel_" + gameObject.name))
		{
			case 0://아예 안깬 스테이지 x표시
				gameObject.GetComponent<Image>().sprite = NONE;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
				break;
			case 1://별이 하나일 때
				gameObject.GetComponent<Image>().sprite = ONE;

				break;
			case 2://별이 두개일 때
				gameObject.GetComponent<Image>().sprite = TWO;

				break;
			case 3://별이 세개일 때
				gameObject.GetComponent<Image>().sprite = THREE;

				break;
			case 4://깨야 할 스테이지인거
				gameObject.GetComponent<Image>().sprite = NEXT;

				break;
			default:

				break;

		}
	}
}
