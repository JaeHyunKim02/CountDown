using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditManager : MonoBehaviour
{
    public static int LeftText;
    public static int AtLeastText;
    public static int[,] TestArray = new int[5, 5];

    [SerializeField]
    private Text text;
    [SerializeField]
    private Text text2;

    private void Start()
    {
        //아무것도 쓰지 않았을때 디폴트 값
        LeftText = 99;
        AtLeastText = 1;
    }

    public void LeftData()
    {
        var temp = text.GetComponent<Text>().text;
        LeftText = int.Parse(temp);
        Debug.Log(LeftText);
    }

    public void AtLeastData()
    {
        var temp = text2.GetComponent<Text>().text;
        AtLeastText = int.Parse(temp);
        Debug.Log(AtLeastText);
    }


}
