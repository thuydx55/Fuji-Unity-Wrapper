using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
using Fuji;
#endif

public class TestUI : MonoBehaviour {
	
    public void onLoginClicked() {
        #if UNITY_ANDROID
        FujiSDK.Instance.Login();

        FujiSDK.Instance.EventLoginSucceed += onLoggedInSucceed;
        FujiSDK.Instance.EventLoginFailed += onLoggedInFailed;
        FujiSDK.Instance.EventLoginCancelled += onLoggedInCancelled;
        #endif
    }	

    private void onLoggedInSucceed() {
        Debug.Log("onLoggedInSucceed");
        removeLoginEvent();
    }

    private void onLoggedInFailed(string message) {
        Debug.Log("onLoggedInFailed");
        removeLoginEvent();
    }

    private void onLoggedInCancelled() {
        Debug.Log("onLoggedInCancelled");
        removeLoginEvent();
    }

    private void removeLoginEvent() {
        #if UNITY_ANDROID
        FujiSDK.Instance.EventLoginSucceed -= onLoggedInSucceed;
        FujiSDK.Instance.EventLoginFailed -= onLoggedInFailed;
        FujiSDK.Instance.EventLoginCancelled -= onLoggedInCancelled;
        #endif
    }

    public void onLogoutClicked() {
        #if UNITY_ANDROID
        FujiSDK.Instance.Logout();
        #endif
    }

    public void onShowUserInfoClicked() {
        #if UNITY_ANDROID
        FujiSDK.Instance.ShowUserInfo();
        #endif
    }

    #if UNITY_ANDROID
    public void onTransferClicked() {
        FujiSDK.Instance.TransferCoin("vn.fujigame.remonster.6");

        FujiSDK.Instance.EventTransferSucceed += onTransferSucceed;
        FujiSDK.Instance.EventTransferFailed += onTransferFailed;
        FujiSDK.Instance.EventTransferCancelled += onTransferCancelled;
    }

    private void onTransferSucceed(Transaction transaction) {
        Debug.Log("Succeed: " + transaction.getPackageCode() + " - " + transaction.getFCoin() + " " + transaction.getPaymentType());
        removeTransferEvent();
    }

    private void onTransferFailed(string msg) {
        Debug.Log("onTransferFailed: " + msg);
        removeTransferEvent();
    }

    private void onTransferCancelled() {
        Debug.Log("onTransferCancelled");
        removeTransferEvent();
    }

    private void removeTransferEvent() {
        FujiSDK.Instance.EventTransferSucceed -= onTransferSucceed;
        FujiSDK.Instance.EventTransferFailed -= onTransferFailed;
        FujiSDK.Instance.EventTransferCancelled -= onTransferCancelled;
    }
    #endif
}
