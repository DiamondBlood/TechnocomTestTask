using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonus : MonoBehaviour
{
    [SerializeField] private GameObject _dailyBonusView;
    [SerializeField] private TMP_Text _dailyBonusText;
    [SerializeField] private TMP_Text _dailyBonusDayText;
    [SerializeField] private Image _dailyBonusProgressbar;
    [SerializeField] private TMP_Text _dailyBonusProgressbarText;
    [SerializeField] private GameObject[] _dailyBonusIcon;
    [SerializeField] private GameObject[] _dailyBonusCollectedIcon;
    [SerializeField] private ResourceManager _resourceManager;
    private const string LastRewardTimeKey = "LastRewardTime";
    private int _day;
    public int rewardIntervalSeconds = 40;
    //Для теста: интервал = 40 секунд
    //24 * 60 * 60
    public void Initialize()
    {
        if (CheckIfRewardAvailable())
        {
            GrantReward();
            PlayerPrefs.SetString(LastRewardTimeKey, DateTime.Now.ToString());
        }
        else
        {
            _day = PlayerPrefs.GetInt("BonusDay");
            UpdateView();
        }
    }

    private bool CheckIfRewardAvailable()
    {
        if (PlayerPrefs.GetInt("BonusDay") == 7) return false;
        string lastRewardTimeString = PlayerPrefs.GetString(LastRewardTimeKey, string.Empty);

        if (!string.IsNullOrEmpty(lastRewardTimeString))
        {
            DateTime lastRewardTime = DateTime.Parse(lastRewardTimeString);
            TimeSpan timeSinceLastReward = DateTime.Now - lastRewardTime;
            if (timeSinceLastReward.TotalSeconds >= rewardIntervalSeconds)
            {
                PlayerPrefs.SetInt("BonusDay", PlayerPrefs.GetInt("BonusDay") + 1);
                PlayerPrefs.Save();
                _day = PlayerPrefs.GetInt("BonusDay");
                return true;
            }
            else return false;
        }
        else
        {
            PlayerPrefs.SetInt("BonusDay", 1);
            PlayerPrefs.Save();
            _day = 1;
            return true;
        }
    }

    private void GrantReward()
    {
        switch (_day)
        {
            case 1: _resourceManager.ChangeTickets(5);
                _dailyBonusText.text = "x" + 5;
                break;
            case 2: _resourceManager.ChangeTickets(5); 
                _dailyBonusText.text = "x" + 5;
                break;
            case 3: _resourceManager.ChangeTickets(10); 
                _dailyBonusText.text = "x" + 10;
                break;
            case 4: _resourceManager.ChangeTickets(10); 
                _dailyBonusText.text = "x" + 10;
                break;
            case 5: _resourceManager.ChangeTickets(15); 
                _dailyBonusText.text = "x" + 15;
                break;
            case 6: _resourceManager.ChangeTickets(15); 
                _dailyBonusText.text = "x" + 15;
                break;
            case 7: _resourceManager.ChangeTickets(25); 
                _dailyBonusText.text = "x" + 25;
                break;
            default:break;
        }
        _dailyBonusDayText.text = "DAY " + _day;
        _dailyBonusView.SetActive(true);
        UpdateView();
    }

    private void UpdateView()
    {
        for (int i = 0; i < _day; i++)
        {
            _dailyBonusIcon[i].SetActive(false);
            _dailyBonusCollectedIcon[i].SetActive(true);
            _dailyBonusProgressbar.fillAmount = (float)_day / 7;
            _dailyBonusProgressbarText.text = _day + "/7";
        }
    }
}
