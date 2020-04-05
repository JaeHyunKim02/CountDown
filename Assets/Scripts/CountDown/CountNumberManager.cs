using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountNumberManager : MonoBehaviour
{
    void ColorChange()
    {
    var TempText = gameObject.GetComponent<Text>();
        var TempInt = int.Parse(TempText.text);
        switch(TempInt)
        {
            case 0:
            case 1:
                TempText.color = new Color(1, 0, 0);
                break;
            case 2:
                TempText.color = new Color(0.8f, 0.15f, 0.15f);
                break;
            case 3:
                TempText.color = new Color(0.8f, 0.2f, 0.2f);
                break;
            case 4:
                TempText.color = new Color(0.7f, 0.2f, 0.2f);
                break;
            case 5:
            case 6:
                TempText.color = new Color(0.6f, 0.2f, 0.2f);
                break;
            case 7:
            case 8:
                TempText.color = new Color(0.5f, 0.25f, 0.25f);
                break;
            case 9:
                TempText.color = new Color(0.4f, 0.345098f, 0.4588235f);
                break;
            default:
                TempText.color = new Color(0.2509804f, 0.2745098f, 0.3529412f);
                break;

        }
    }

    void SizeChange()
    {
        var TempText = gameObject.GetComponent<Text>();
        var TempInt = int.Parse(TempText.text);

        switch (TempInt)
        {
            case 0:
                TempText.fontSize = 180;
                break;
            case 1:
                TempText.fontSize = 180;
                break;
            case 2:
                TempText.fontSize = 175;
                break;
            case 3:
                TempText.fontSize = 170;
                break;
            case 4:
                TempText.fontSize = 168;
                break;
            case 5:
            case 6:
                TempText.fontSize = 166;
                break;
            case 7:
            case 8:
                TempText.fontSize = 164;
                break;
            case 9:
                TempText.fontSize = 162;
                break;
            default:
                TempText.fontSize = 160;
                break;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        ColorChange();
        SizeChange();
    }

}
