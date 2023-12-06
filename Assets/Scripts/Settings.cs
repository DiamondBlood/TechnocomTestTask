using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Image _musicIcon;
    [SerializeField] private Sprite _musicIconEnabled;
    [SerializeField] private Sprite _musicIconDisabled;
    [SerializeField] private Image _soundIcon;
    [SerializeField] private Sprite _soundIconEnabled;
    [SerializeField] private Sprite _soundIconDisabled;

    public void Start()
    {
        if (PlayerPrefs.GetInt("Music",1)==0) MusicOff();
        else                                  MusicOn();
        if (PlayerPrefs.GetInt("Sound",1)==0) SoundsOff();
        else                                  SoundsOn();
    }

    public void MusicSet()
    {
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            MusicOff();
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            MusicOn();
            PlayerPrefs.SetInt("Music", 1);
        }
        PlayerPrefs.Save();
    }

    public void SoundsSet()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            SoundsOff();
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            SoundsOn();
            PlayerPrefs.SetInt("Sound", 1);
        }
        PlayerPrefs.Save();
    }

    private void MusicOn()
    {
        _musicIcon.sprite = _musicIconEnabled;
        _audioMixer.SetFloat("MusicVolume", 0f);
    }
    private void MusicOff()
    {
        _musicIcon.sprite = _musicIconDisabled;
        _audioMixer.SetFloat("MusicVolume", -80f);
    }
    private void SoundsOn()
    {
        _soundIcon.sprite = _soundIconEnabled;
        _audioMixer.SetFloat("SoundsVolume", 0f);
    }
    private void SoundsOff()
    {
        _soundIcon.sprite = _soundIconDisabled;
        _audioMixer.SetFloat("SoundsVolume", -80f);
    }
}
