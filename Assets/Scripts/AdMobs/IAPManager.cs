using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public const string ProductAdRemover = "NoAd";
    private const string _android_Ad = "com.BeefStewdio.app.ad";

    private static IAPManager m_instance;

    //public static IAPManager Instance
    //{
    //    get
    //    {
    //        if (m_instance != null)
    //            return m_instance;

    //        m_instance = FindObjectOfType<IAPManager>();

    //        if (m_instance == null)
    //            m_instance = new GameObject(name: "Purchase").AddComponent<IAPManager>();
    //        return m_instance;
    //    }
    //}
        private IStoreController storeConsroller;
        private IExtensionProvider storeExtensionProvider;

    public bool IsInitialized => storeConsroller != null && storeExtensionProvider != null;

        void Awake()
        {
            if(m_instance != null && m_instance != null)
            {
                Destroy(gameObject);
               return;
            }
            InitUnityIAP();
        }

    void InitUnityIAP()
    {
        if (IsInitialized) return;
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(
            id: ProductAdRemover, ProductType.NonConsumable, new IDs()
                {
                    {_android_Ad, GooglePlay.Name}
                }
            );
        UnityPurchasing.Initialize(listener:this,builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log(message: "유니티 IAP 초기화 성공");
        storeConsroller = controller;
        storeExtensionProvider = extensions;
        
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError(message: $"유니티 IAP 초기화 실패 {error}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log(message: $"구매 성공 - ID : {args.purchasedProduct.definition.id}");

        if (args.purchasedProduct.definition.id == ProductAdRemover)
        {
            myStatic.IsAdRemove = true;
            PlayerPrefs.SetInt("NoAd", 1);
            PlayerPrefs.Save();
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(message: $"구매실패 - {product.definition.id}, {reason}");
    }

    public void Purchase(string productId)
    {
        if (!IsInitialized) return;

        var product = storeConsroller.products.WithID(productId);

        if (product != null && product.availableToPurchase)
        {
            Debug.Log(message: $"구매 시도 -{product.definition.id}");
            storeConsroller.InitiatePurchase(product);
        }
        else
        {
            Debug.Log(message: $"구매 시도 불가 - {productId}");
        }
    }

    public void RestorePurchase()
    {
        if (!IsInitialized) return;

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log(message: "구매 복구 시도");

            var appleExt = storeExtensionProvider.GetExtension<IAppleExtensions>();

            appleExt.RestoreTransactions(
                callback: result => Debug.Log(message: $"구매 복구 시도 결과 - {result}"));
        }
    }
    public bool HadPurchased(string productId)
    {
        if (!IsInitialized) return false;

        var product = storeConsroller.products.WithID(productId);

        if (product != null)
        {
            return product.hasReceipt;
        }

        return false; 
    }

}
