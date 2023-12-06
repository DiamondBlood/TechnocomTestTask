using UnityEngine;
using UnityEngine.UI;

public class ShopItemsStatus : MonoBehaviour
{
    [SerializeField] private GameObject _skin2LockedIcon;
    [SerializeField] private GameObject _skin2UnlockedIcon;
    [SerializeField] private Button _skin2Button;

    [SerializeField] private GameObject _location2LockedIcon;
    [SerializeField] private GameObject _location2UnlockedIcon;
    [SerializeField] private Button _location2Button;

    [SerializeField] private GameObject _location3LockedIcon;
    [SerializeField] private GameObject _location3UnlockedIcon;
    [SerializeField] private Button _location3Button;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("LevelsPassed",0) >= 7)
        {
            _location2LockedIcon.SetActive(false);
            _location2UnlockedIcon.SetActive(true);
            _location2Button.interactable = true;
        }
        if (PlayerPrefs.GetInt("LevelsPassed",0) >= 14)
        {
            _location3LockedIcon.SetActive(false);
            _location3UnlockedIcon.SetActive(true);
            _location3Button.interactable = true;
        }
        if (PlayerPrefs.GetInt("LevelsPassed",0) >= 10)
        {
            _skin2LockedIcon.SetActive(false);
            _skin2UnlockedIcon.SetActive(true);
            _skin2Button.interactable = true;
        }
    }
}
