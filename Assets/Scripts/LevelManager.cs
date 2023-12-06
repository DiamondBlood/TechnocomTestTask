using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _lockedIcons;
    [SerializeField] private GameObject[] _unlockedIcons;

    private void Start()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("LevelsPassed",0); i++)
        {
            _lockedIcons[i].SetActive(false);
            _unlockedIcons[i].SetActive(true);
        }
    }

    public void PassLevelLoc1(int levelID)
    {
        PassLevel(levelID-1);
    }
    public void PassLevelLoc2(int levelID)
    {
        if (PlayerPrefs.GetInt("Location2Bought",0)==1)
            PassLevel(levelID-1);
    }
    public void PassLevelLoc3(int levelID)
    {
        if (PlayerPrefs.GetInt("Location3Bought", 0) == 1)
            PassLevel(levelID - 1);
    }

    private void PassLevel(int levelID)
    {
        _lockedIcons[levelID].SetActive(false);
        _unlockedIcons[levelID].SetActive(true);
        PlayerPrefs.SetInt("LevelsPassed", levelID+1);
        PlayerPrefs.Save();
    }
}
