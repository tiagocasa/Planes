using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private float volumeGeral;
    private bool isSuMute;
    private bool isMuMute;
    public Slider musicSlider;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public Toggle soundSu;
    public Toggle soundMu;



    public void Start()
    {
        /// Starta Definindo se som e efeitos ficam ligados ou desligados
        volumeGeral = PlayerPrefs.GetFloat("VolumeMaster", 0f);

        if (PlayerPrefs.GetInt("SoundMute") == 1)
        {
            //Mutado
            sfxMixer.SetFloat("SFXVolume", -80f);
            soundSu.isOn = true;
        }
        else
        {
            sfxMixer.SetFloat("SFXVolume", volumeGeral);
            soundSu.isOn = false;

        }

        if (PlayerPrefs.GetInt("MusicMute") == 1)
        {
            //Mutado
            musicMixer.SetFloat("MusicVolume", -80f);
            soundMu.isOn = true;
        }
        else
        {
            musicMixer.SetFloat("MusicVolume", volumeGeral);
            soundMu.isOn = false;
        }

        
        
      //  FindObjectOfType<AudioManagerMenu>().Play("Menu");
        musicSlider.value = volumeGeral;

    }
    public void Awake()
    {
        
        
    }

    public void SetVolume (float volume)
    {
        if (isMuMute == true)
        {
            volumeGeral = PlayerPrefs.GetFloat("VolumeMaster", volume);
        }
        else
        {

        musicMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("VolumeMaster", volume);
        volumeGeral = PlayerPrefs.GetFloat("VolumeMaster", volume);
        }

        if (isSuMute == true)
        {
            volumeGeral = PlayerPrefs.GetFloat("VolumeMaster", volume);
        }
        else
        {
            sfxMixer.SetFloat("SFXVolume", volume);
            PlayerPrefs.SetFloat("VolumeMaster", volume);
            volumeGeral = PlayerPrefs.GetFloat("VolumeMaster", volume);
        }

    }

    public void MuteSound (bool isSoundMute)
    {
       
        if (isSoundMute == true)
        {
            PlayerPrefs.SetInt("SoundMute", 1);
            sfxMixer.SetFloat("SFXVolume", -80f);
            isSuMute = true;

        }
        else
        {
            PlayerPrefs.SetInt("SoundMute", 0);
            sfxMixer.SetFloat("SFXVolume", volumeGeral);
            isSuMute = false;
        }

    }

    public void MuteMusic(bool isMusicMute)
    {

        if (isMusicMute == true)
        {
            PlayerPrefs.SetInt("MusicMute", 1);
            musicMixer.SetFloat("MusicVolume", -80f);
            isMuMute = true;

        }
        else
        {
            PlayerPrefs.SetInt("MusicMute", 0);
            musicMixer.SetFloat("MusicVolume", volumeGeral);
            isMuMute = false;
        }

    }

}
