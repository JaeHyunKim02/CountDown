using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] GameObject StageObject;
    [SerializeField] int StageAmount;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] stageamount = new GameObject[StageAmount];
        for (int i = 1; i <= StageAmount; i++)
        {
            stageamount[i-1] = (GameObject)Instantiate(StageObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), gameObject.transform);

            stageamount[i-1].name = i.ToString();
            stageamount[i-1].transform.GetChild(0).transform.GetComponent<Text>().text = i.ToString();
        }

        //여기 콘텐츠 사이즈 키우는 코드
        //gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, Mathf.Abs(stageamount[StageAmount - 1].GetComponent<RectTransform>().anchoredPosition.y) + 80);
        //Debug.Log(stageamount[StageAmount - 1].GetComponent<RectTransform>().anchoredPosition.y);//이거 문제임. 몇 초뒤에 정렬되기때문에 Start즉시 하는건 포지션값에 무리무리요
    }
}
