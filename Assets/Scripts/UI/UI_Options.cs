using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Options : MonoBehaviour
{
    private Player player;
    [SerializeField] private Toggle healthBarToggle;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mixerMultiplier = 25;

    [Header("BGM Volume Settings")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private string bgmParametr;

    [Header("SFX Volume Settings")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private string sfxPamametr;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();


        healthBarToggle.onValueChanged.AddListener(OnHealthBarToggleChanged);
    }

    public void BGMSliderValue(float value)
    {
        float newValue = Mathf.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(bgmParametr, newValue);
    }
    
    public void SFXSliderValue(float value)
    {
        float newValue = Mathf.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(sfxPamametr, newValue);
    }

    private void OnHealthBarToggleChanged(bool isOn)
    {
        player.health.EnableHealthBar(isOn);
    }

    public void GoMainMenuBTN() => GameManager.instance.ChangeScene("MainMenu", RespawnType.NonSpecific);

    private void OnEnable()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(sfxPamametr, 0.6f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParametr, 0.6f);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(sfxPamametr, sfxSlider.value);
        PlayerPrefs.SetFloat(bgmParametr, bgmSlider.value);
    }

    public void LoadUpVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(sfxPamametr, 0.6f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParametr, 0.6f);
    }
}
