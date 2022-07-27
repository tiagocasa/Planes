using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;

public class RemoteConfigsManager : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttibrutes { }

    public GameObject playStore;

    public int gameVersionBundle;
    //[Header("Fallback Configs Data")]
    //public FallbackConfigsData fallbackDefaultConfig;
    void Awake()
    {
        FetchRemoteConfiguration();
    }

    public void FetchRemoteConfiguration()
    {
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        ConfigManager.FetchConfigs<userAttributes, appAttibrutes>(new userAttributes(), new appAttibrutes());
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                break;
            case ConfigOrigin.Cached:
                break;
            case ConfigOrigin.Remote:
                SetVersion();
                break;

        }
    }

    void SetVersion()
    {
        int version = ConfigManager.appConfig.GetInt("versao");
        if (version > gameVersionBundle)
        {
            playStore.SetActive(true);
        }
    }

    public void OpenPlayStore()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.RollingHouseStudo.ClickerVillage");
    }
}
