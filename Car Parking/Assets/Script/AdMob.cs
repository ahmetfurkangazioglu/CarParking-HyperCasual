using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace AdManager
{
    public class AdMobManager
    {
        private InterstitialAd interstitial;
        private RewardedAd rewarded;
        private RewardedAd revive;
        public void RequestInterstitialAd()
        {
            string adUnityId;
#if UNITY_ANDROID

            adUnityId = "ca-app-pub-3940256099942544/1033173712";
#else
            adUnityId = "unexpected_platform";
#endif

            interstitial = new InterstitialAd(adUnityId);
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
            interstitial.OnAdClosed += InterstitialClosed;

        }
        void InterstitialClosed(object sender, EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitialAd();
        }
        public void ShowInterstitial()
        {
            if (PlayerPrefs.GetInt("CurrentAd") == 1)
            {
                if (interstitial.IsLoaded())
                {
                    interstitial.Show();
                    PlayerPrefs.SetInt("CurrentAd", 0);
                }
                else
                {
                    interstitial.Destroy();
                    RequestInterstitialAd();
                }

            }
            else
            {
                PlayerPrefs.SetInt("CurrentAd", PlayerPrefs.GetInt("CurrentAd") + 1);
            }

        }


        public void RequestRewardAd()
        {
            string adUnityId;
#if UNITY_ANDROID

            adUnityId = "ca-app-pub-3940256099942544/5224354917";
#else
            adUnityId = "unexpected_platform";
#endif

            rewarded = new RewardedAd(adUnityId);
            AdRequest request = new AdRequest.Builder().Build();
            rewarded.LoadAd(request);
            rewarded.OnUserEarnedReward += UserEarnedReward;
            rewarded.OnAdClosed += RewardClosed;
        }
        public void ShowReward()
        {
            if (rewarded.IsLoaded())
            {
                rewarded.Show();
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().RewardsOperation("DisableDiamond");
            }
            else
            {
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().RewardsOperation("DisableDiamond");
            }
        }
        private void UserEarnedReward(object sender, Reward e)
        {
            string type = e.Type;
            double amount = e.Amount;
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DiamondRewars(amount);
        }
        private void RewardClosed(object sender, EventArgs e)
        {
            RequestRewardAd();
        }


        public void RequestReviveRewardAd()
        {
            string adUnityId;
#if UNITY_ANDROID

            adUnityId = "ca-app-pub-3940256099942544/5224354917";
#else
            adUnityId = "unexpected_platform";
#endif

            revive = new RewardedAd(adUnityId);
            AdRequest request = new AdRequest.Builder().Build();
            revive.LoadAd(request);
            revive.OnUserEarnedReward += UserEarnedReviveReward;
            revive.OnAdClosed += RewardReviveClosed;
        }
        public void ShowReviveReward()
        {
            if (revive.IsLoaded())
            {
                revive.Show();
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().RewardsOperation("DisableRevive");
            }
            else
            {
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().RewardsOperation("DisableRevive");
            }
        }
        private void UserEarnedReviveReward(object sender, Reward e)
        {
            string type = e.Type;
            double amount = e.Amount;
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().ReviveRewars();
        }
        private void RewardReviveClosed(object sender, EventArgs e)
        {
            RequestRewardAd();
        }
    }
}