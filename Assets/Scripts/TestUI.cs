using UnityEngine;
using System.Collections;
using Fuji;

public class TestUI : MonoBehaviour {
	
    public void onLoginClicked() {
        FujiSDK.Instance.Login();

        FujiSDK.Instance.EventLoginSucceed += onLoggedInSucceed;
        FujiSDK.Instance.EventLoginFailed += onLoggedInFailed;
        FujiSDK.Instance.EventLoginCancelled += onLoggedInCancelled;
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
        FujiSDK.Instance.EventLoginSucceed -= onLoggedInSucceed;
        FujiSDK.Instance.EventLoginFailed -= onLoggedInFailed;
        FujiSDK.Instance.EventLoginCancelled -= onLoggedInCancelled;
    }

    public void onLogoutClicked() {
        FujiSDK.Instance.Logout();
    }

    public void onShowUserInfoClicked() {
        FujiSDK.Instance.ShowUserInfo();
    }

    public void onTransferClicked() {
        FujiSDK.Instance.TransferCoin("jp.co.alphapolis.games.remon.5");

        FujiSDK.Instance.EventTransferSucceed += onTransferSucceed;
        FujiSDK.Instance.EventTransferFailed += onTransferFailed;
        FujiSDK.Instance.EventTransferCancelled += onTransferCancelled;
    }

    private void onTransferSucceed(Transaction transaction) {
        Debug.Log("Succeed: " + transaction.getPackageCode() + " - " + transaction.getFCoin() + " " + transaction.getPaymentType());
        removeTransferEvent();
    }

    private void onTransferFailed(string msg) {
        Debug.Log("onTransferFailed");
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
}
