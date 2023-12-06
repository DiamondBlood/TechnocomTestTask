using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class IAPCore : MonoBehaviour, IDetailedStoreListener
{
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private GameObject _skin1Cost;
    [SerializeField] private GameObject _skin1BoughtIcon;
    [SerializeField] private Button _skin1Button;
    [SerializeField] private GameObject _skin2Cost;
    [SerializeField] private GameObject _skin2BoughtIcon;
    [SerializeField] private Button _skin2Button;
    [SerializeField] private GameObject _location2Cost;
    [SerializeField] private GameObject _location2BoughtIcon;
    [SerializeField] private Button _location2Button;
    [SerializeField] private GameObject _location3Cost;
    [SerializeField] private GameObject _location3BoughtIcon;
    [SerializeField] private Button _location3Button;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public static string epicChest = "com.diamond.technocomtesttask.epicchest";
    public static string luckyChest = "com.diamond.technocomtesttask.luckychest";
   

    private void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
        if (PlayerPrefs.GetInt("Skin1", 0) == 1)
        {
            _skin1Cost.SetActive(false);
            _skin1BoughtIcon.SetActive(true);
            _skin1Button.interactable = false;
        }
        if (PlayerPrefs.GetInt("Skin2", 0) == 1)
        {
            _skin2Cost.SetActive(false);
            _skin2BoughtIcon.SetActive(true);
            _skin2Button.interactable = false;
        }
        if (PlayerPrefs.GetInt("Location2Bought", 0) == 1)
        {
            _location2Cost.SetActive(false);
            _location2BoughtIcon.SetActive(true);
            _location2Button.interactable = false;
        }
        if (PlayerPrefs.GetInt("Location3Bought", 0) == 1)
        {
            _location3Cost.SetActive(false);
            _location3BoughtIcon.SetActive(true);
            _location3Button.interactable = false;
        }
    }
    #region IAP
    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(epicChest, ProductType.Consumable);
        builder.AddProduct(luckyChest, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyEpicChest() => BuyProductID(epicChest);
    public void BuyLuckyChest() => BuyProductID(luckyChest);

    void BuyProductID(string productId)
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
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, epicChest, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            _resourceManager.ChangeTickets(500);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, luckyChest, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            _resourceManager.ChangeTickets(1200);
        }

        return PurchaseProcessingResult.Complete;
    }


    //Resore Purchases
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;

    }

    public void RestorePurchases()
    {
        m_StoreExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions((result, error) =>
        {
            if (result)
            {
                // This does not mean anything was restored,
                // merely that the restoration process succeeded.
            }
            else
            {
                // Restoration failed. `error` contains the failure reason.
            }
        });
    }
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error + ". Message: " + message);
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
    #endregion

    public void BuySkin1()
    {
        if (PlayerPrefs.GetInt("TicketsCount", 0)>=250)
        {
            _resourceManager.ChangeTickets(-250);
            _skin1Cost.SetActive(false);
            _skin1BoughtIcon.SetActive(true);
            _skin1Button.interactable = false;
            PlayerPrefs.SetInt("Skin1", 1);
            PlayerPrefs.Save();
        }
    }
    public void BuySkin2()
    {
        if (PlayerPrefs.GetInt("TicketsCount", 0)>=500)
        {
            _resourceManager.ChangeTickets(-500);
            _skin2Cost.SetActive(false);
            _skin2BoughtIcon.SetActive(true);
            _skin2Button.interactable = false;
            PlayerPrefs.SetInt("Skin2", 1);
            PlayerPrefs.Save();
        }
    }
    public void BuyLocation2()
    {
        if (PlayerPrefs.GetInt("TicketsCount", 0) >= 500)
        {
            _resourceManager.ChangeTickets(-500);
            _location2Cost.SetActive(false);
            _location2BoughtIcon.SetActive(true);
            _location2Button.interactable = false;
            PlayerPrefs.SetInt("Location2Bought", 1);
            PlayerPrefs.Save();
        }
    }
    public void BuyLocation3()
    {
        if (PlayerPrefs.GetInt("TicketsCount", 0) >= 500)
        {
            _resourceManager.ChangeTickets(-500);
            _location3Cost.SetActive(false);
            _location3BoughtIcon.SetActive(true);
            _location3Button.interactable = false;
            PlayerPrefs.SetInt("Location3Bought", 1);
            PlayerPrefs.Save();
        }
    }
}
