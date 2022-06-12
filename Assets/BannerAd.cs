﻿using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{

    public string gameId = "3782987";
    public string placementId = "Banner";
    public bool testMode = true;

    void Start()
    {
        // Initialize the SDK if you haven't already done so:
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

        Advertisement.Banner.Show(placementId);
    }
}