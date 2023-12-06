using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _ticketsValueText;

    private int _ticketsValue;
    public void Initialize()
    {
        _ticketsValue = PlayerPrefs.GetInt("TicketsCount", 0);
        _ticketsValueText.text = _ticketsValue.ToString();
    }
    public void ChangeTickets(int value)
    {
        _ticketsValue += value;
        _ticketsValueText.text = _ticketsValue.ToString();
        PlayerPrefs.SetInt("TicketsCount", _ticketsValue);
        PlayerPrefs.Save();
    }
}
