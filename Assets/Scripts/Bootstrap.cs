using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private DailyBonus _dailyBonus;
    private void Start()
    {
        _resourceManager.Initialize();
        _dailyBonus.Initialize();
    }
}
