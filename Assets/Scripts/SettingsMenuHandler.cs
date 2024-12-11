using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private Slider M_slider;
    [SerializeField] private Slider SFX_slider;
    [SerializeField] private Slider OST_slider;
    [SerializeField] private Toggle MMute;
    [SerializeField] private Toggle SFXMute;
    [SerializeField] private Toggle OSTMute;
    [SerializeField] private GameObject MenuFrame;
    [SerializeField] private GameObject CursorFrame;

    float _baseSpeed;
    float _desiredSpeed;

    private void Awake()
    {
        _baseSpeed = Time.timeScale; 
        _desiredSpeed = Time.timeScale;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuFrame.SetActive(!MenuFrame.activeInHierarchy);
            CursorFrame.SetActive(!MenuFrame.activeInHierarchy);
            if (!MenuFrame.activeInHierarchy)
            {
                _desiredSpeed = _baseSpeed; // returns speed to default if not active
            }
            else
            {
                _desiredSpeed = 0; // Za warudo.
            }
        }
        if (AudioMixer != null)
        {
            float mastervolume = MMute.isOn ? -80 : M_slider.value;
            float ostvolume = OSTMute.isOn ? -80 : OST_slider.value;
            float sfxvolume = SFXMute.isOn ? -80 : SFX_slider.value;
            AudioMixer.SetFloat("MVolume", Math.Clamp(mastervolume, -80f, 0f));
            AudioMixer.SetFloat("MusicVolume", Math.Clamp(ostvolume, -80f, 0f));
            AudioMixer.SetFloat("SFXVolume", Math.Clamp(sfxvolume, -80f, 0f));
        }
        Time.timeScale = _desiredSpeed;
    }
}
