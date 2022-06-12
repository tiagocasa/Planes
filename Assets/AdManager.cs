using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string playstoreID = "3782987";
    private string appstoreID = "3782986";

    private string interAD = "video";
    private string rewardAD = "rewardedVideo";
    private string bannerAD = "Banner";

    public bool isTargetPlayStore;
    public bool isTestAd;

    public Text coinText;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public Animator continueanim;

    private void Start()
    {
        Advertisement.AddListener(this);
        InitialAD();
    }

    private void InitialAD()
    {
        if (isTargetPlayStore) { Advertisement.Initialize(playstoreID, isTestAd);return; }
        Advertisement.Initialize(appstoreID, isTestAd);
    }

    public void PlayInterAD()
    {
        if (!Advertisement.IsReady(interAD)) { return; }
        if (PlayerPrefs.GetInt("Continue", 0) == 1)
        {
            Advertisement.Show(interAD);
        }
        else
        {
            continueanim.SetTrigger("ContinueAn");
            FindObjectOfType<AudioManager>().Play("voltar");
        }
    }

    public void PlayRewardAD()
    {
        if (!Advertisement.IsReady(rewardAD)) { return; }
        Advertisement.Show(rewardAD);
    }





    public void OnUnityAdsReady(string placmentId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {

    }


    public void OnUnityAdsDidStart(string placementId)
    {
        sfxMixer.SetFloat("SFXVolume", -80f);
        musicMixer.SetFloat("MusicVolume", -80f);
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:
                if(placementId == rewardAD)
                {
                    int coinsganhar = PlayerPrefs.GetInt("Coins", 0);
                    coinsganhar += 10;
                    PlayerPrefs.SetInt("Coins", coinsganhar);
                    coinText.text = "MOEDAS:" + PlayerPrefs.GetInt("Coins", 0).ToString();

                }
                if (placementId == interAD)
                {
                    PlayerPrefs.SetInt("Continue", 1);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
        }

        sfxMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("VolumeMaster", 0f));
        musicMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("VolumeMaster", 0f));
    }
}
