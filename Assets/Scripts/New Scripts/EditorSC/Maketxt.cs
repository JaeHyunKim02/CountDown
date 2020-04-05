using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Maketxt : MonoBehaviour
{
    string source = ""; //읽어낸 텍스트 할당받는 변수

    public static void WriteData(string strData)
    {
        // FileMode.Create는 덮어쓰기.
        FileStream f = new FileStream("./" + "MapText.txt", FileMode.Create, FileAccess.Write);

        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        writer.WriteLine(strData);
        writer.Close();
    }
}