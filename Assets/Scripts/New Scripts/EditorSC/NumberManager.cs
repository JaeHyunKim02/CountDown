using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Data1()
    {
        string[] split_text = gameObject.transform.parent.name.Split('_');
        EditManager.TestArray[int.Parse(split_text[1]), int.Parse(split_text[2])] = 1;
        Debug.Log("I saved " + "TestArray[" + int.Parse(split_text[1]) + "][" + int.Parse(split_text[2]) + "]" + "and 1");
    }

    public void Data2()
    {
        string[] split_text = gameObject.transform.parent.name.Split('_');
        EditManager.TestArray[int.Parse(split_text[1]), int.Parse(split_text[2])] = 2;
        Debug.Log("I saved " + "TestArray[" + int.Parse(split_text[1]) + "][" + int.Parse(split_text[2]) + "]" + "and 2");
    }

    public void Data3()
    {
        string[] split_text = gameObject.transform.parent.name.Split('_');
        EditManager.TestArray[int.Parse(split_text[1]), int.Parse(split_text[2])] = 3;
        Debug.Log("I saved " + "TestArray[" + int.Parse(split_text[1]) + "][" + int.Parse(split_text[2]) + "]" + "and 3");
    }

    public void Data4()
    {
        string[] split_text = gameObject.transform.parent.name.Split('_');
        EditManager.TestArray[int.Parse(split_text[1]), int.Parse(split_text[2])] = 4;
        Debug.Log("I saved " + "TestArray[" + int.Parse(split_text[1]) + "][" + int.Parse(split_text[2]) + "]" + "and 4");
    }

    public void DataX()
    {
        string[] split_text = gameObject.transform.parent.name.Split('_');
        EditManager.TestArray[int.Parse(split_text[1]), int.Parse(split_text[2])] = 255;
        Debug.Log("I saved " + "TestArray[" + int.Parse(split_text[1]) + "][" + int.Parse(split_text[2]) + "]" + "and 255");
    }

    public void DataNone()
    {
        string[] split_text = gameObject.transform.parent.name.Split('_');
        EditManager.TestArray[int.Parse(split_text[1]), int.Parse(split_text[2])] = 0;
        Debug.Log("I saved " + "TestArray[" + int.Parse(split_text[1]) + "][" + int.Parse(split_text[2]) + "]" + "and 0");
    }

    public void DataTexting()
    {
        var temp = transform.GetChild(2).gameObject.GetComponent<Text>().text;
        string[] split_text = gameObject.transform.parent.name.Split('_');
        EditManager.TestArray[int.Parse(split_text[1]), int.Parse(split_text[2])] = int.Parse(temp);
        Debug.Log("I saved " + "TestArray[" + int.Parse(split_text[1]) + "][" + int.Parse(split_text[2]) + "]" + "and " + temp);
    }
}
