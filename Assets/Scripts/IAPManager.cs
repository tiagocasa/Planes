using System;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;

    public AddCashCoin mg;
    public GameObject buttontest;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products
    private string btn1 = "button1";
    private string btn2 = "button2";
    private string btn3 = "button3";
    private string btn4 = "button4";


    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            mg = FindObjectOfType<AddCashCoin>();
            buttontest = FindObjectOfType<NewMenu>().transform.GetChild(7).GetChild(0).GetChild(1).GetChild(2).GetChild(0).gameObject;
            buttontest.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Buy1(); });
            buttontest.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Buy2(); });
            buttontest.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Buy3(); });
            buttontest.transform.GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Buy4(); });
        }
    }
    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(btn1, ProductType.Consumable);
        builder.AddProduct(btn2, ProductType.Consumable);
        builder.AddProduct(btn3, ProductType.Consumable);
        builder.AddProduct(btn4, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods
    public void Buy1()
    {
        BuyProductID(btn1);
    }

    public void Buy2()
    {
        BuyProductID(btn2);
    }
    public void Buy3()
    {
        BuyProductID(btn3);
    }
    public void Buy4()
    {
        BuyProductID(btn4);
    }


    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, btn1, StringComparison.Ordinal))
        {
            mg.BuyCash1();
            Debug.Log("Premium Upgrade");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, btn2, StringComparison.Ordinal))
        {
            mg.BuyCash2();
            Debug.Log("Deluxe Upgrade");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, btn3, StringComparison.Ordinal))
        {
            mg.BuyCash3();
            Debug.Log("Deluxe Upgrade");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, btn4, StringComparison.Ordinal))
        {
            mg.BuyCash4();
            Debug.Log("Deluxe Upgrade");
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }










    //**************************** Dont worry about these methods ***********************************
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
       
        //price1Text.text = m_StoreController.products.WithID("button1").metadata.localizedPriceString;
        //Debug.Log(m_StoreController.products.WithID("button1").metadata.localizedPriceString);
        //price2Text.text = m_StoreController.products.WithID("button2").metadata.localizedPriceString;
        //price3Text.text = m_StoreController.products.WithID("button3").metadata.localizedPriceString;
        //price4Text.text = m_StoreController.products.WithID("button4").metadata.localizedPriceString;


        MenuManager.instance.btn1 = m_StoreController.products.WithID("button1").metadata.localizedPriceString;
        MenuManager.instance.btn2 = m_StoreController.products.WithID("button2").metadata.localizedPriceString;
        MenuManager.instance.btn3 = m_StoreController.products.WithID("button3").metadata.localizedPriceString;
        MenuManager.instance.btn4 = m_StoreController.products.WithID("button4").metadata.localizedPriceString;







        //if (controller != null)
        //{
        //    // Fetch the currency Product reference from Unity Purchasing
        //    Product product = controller.products.WithID(btn1);
        //    if (product != null && product.hasReceipt)
        //    {
        //        Debug.Log("Botao Desligado");
        //        Debug.Log(product.metadata.localizedTitle);


        //        //        PlayerPrefs.SetInt("isPremium", 1);
        //    }
        //    else
        //    {
        //        Debug.Log(product.metadata.localizedTitle);
        //        Debug.Log("Botao Ligado");
        //    }

        //    // Fetch the currency Product reference from Unity Purchasing
        //    Product product2 = controller.products.WithID(btn2);
        //    if (product2 != null && product2.hasReceipt)
        //    {
        //        Debug.Log("Botao Desligado deluxe");
        //        Debug.Log(product2.metadata.localizedTitle);

        //        //        PlayerPrefs.SetInt("isPremium", 1);
        //    }
        //    else
        //    {
        //        Debug.Log(product2.metadata.localizedTitle);
        //        Debug.Log("Botao Ligado");
        //    }
        //}
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}