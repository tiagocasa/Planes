using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private float volumeGeral;
    private float volumeSFX;
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public GameObject tela;



    public void Start()
    {
        /// Starta Definindo se som e efeitos ficam ligados ou desligados
        volumeGeral = PlayerPrefs.GetFloat("MusicVolume", 20f);
        volumeSFX = PlayerPrefs.GetFloat("SFXVolume", 20f);

  
        sfxMixer.SetFloat("SFXVolume", volumeSFX);
        musicMixer.SetFloat("MusicVolume", volumeGeral);

      //  FindObjectOfType<AudioManager>().Play("Menu");
        musicSlider.value = volumeGeral;
        sfxSlider.value = volumeSFX;
        tela.SetActive(false);
    }
    public void SetMusic ()
    {
        volumeGeral = musicSlider.value;
        musicMixer.SetFloat("MusicVolume", volumeGeral);
        PlayerPrefs.SetFloat("MusicVolume", volumeGeral);
    }

    public void SetSFX()
    {
        volumeGeral = sfxSlider.value;
        sfxMixer.SetFloat("SFXVolume", volumeGeral);
        PlayerPrefs.SetFloat("SFXVolume", volumeGeral);
    }




}
