using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour
{
    private const string subject = "2048을 넘어선 뛰어난형태의 퍼즐! CountDown";
    private const string body = "http://www.smilegatefoundation.org/youth/eventDetail?CONT_SEQ=369";
    public GameObject OptionWindow;//옵션창

    public string targetProductId;//구매용 상품 id

    public void ClickSetting()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        OptionWindow.SetActive(true);
    }

    //public void ClickShop()
    //{
    //    GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");

    //    if (targetProductId == IAPManager.ProductAdRemover)
    //    {
    //        if (IAPManager.Instance.HadPurchased(targetProductId))
    //        {
    //            Debug.Log(message: "이미 구매한 상품");
    //            return;
    //        }
    //    }
    //        IAPManager.Instance.RestorePurchase();

    //        IAPManager.Instance.Purchase(targetProductId);
    //}

    public void ClickLike()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        Application.OpenURL("https://www.facebook.com/COUNT-DOWN-428553977998794/?modal=admin_todo_tour");
    }

    public void ClickShare()
    {
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent")) 
        using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent")) {
            intentObject.Call<AndroidJavaObject>("setAction", intentObject.GetStatic<string>("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_SUBJECT"), subject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_TEXT"), body);
            using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity")) 
            using (AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via"))
            currentActivity.Call("startActivity", jChooser);
        }
#endif
    }

}
