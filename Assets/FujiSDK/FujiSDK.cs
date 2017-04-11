using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_ANDROID
namespace Fuji {
    public delegate void ObjectHandler<T>(T obj);
    public delegate void StringHandler(string msg);
    public delegate void VoidHandler();

    public class FujiSDK : MonoBehaviour {

        private static FujiSDK instance = null;
        private static readonly object padlock = new object();

        private AndroidJavaObject mFujiInstance;

        public static FujiSDK Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new FujiSDK();
                        }
                    }
                }
                return instance;
            }
        }

        public bool debugMode = false;
        public string appId = "";

        public event VoidHandler EventLoginSucceed;
        public event StringHandler EventLoginFailed;
        public event VoidHandler EventLoginCancelled;

        public event ObjectHandler<Transaction> EventTransferSucceed;
        public event StringHandler EventTransferFailed;
        public event VoidHandler EventTransferCancelled;

        public event ObjectHandler<List<string> > EventPaymentChannelSucceed;
        public event StringHandler EventPaymentChannelFailed;

        void Awake() {
            DontDestroyOnLoad(this);
            instance = this;
        }
    	// Use this for initialization
    	void Start () {
            var fujiClass = new AndroidJavaClass("com.fuji.fujisdk.FujiSDK"); 
            mFujiInstance = fujiClass.GetStatic<AndroidJavaObject>("Instance");

            var androidActivity = GetAndroidActivity();
            var androidApplication = androidActivity.Call<AndroidJavaObject>("getApplication");

            mFujiInstance.Call("setDebugMode", debugMode);
            mFujiInstance.Call("initialize", androidApplication, appId);
    	}

        private AndroidJavaObject GetAndroidActivity()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            return jo;
        }

        public bool IsLoggedIn() {
            if (mFujiInstance != null)
                return mFujiInstance.Call<bool>("isLoggedIn");
            return false;
        }

        public bool IsStorePaymentReady() {
            if (mFujiInstance != null)
                return mFujiInstance.Call<bool>("isStorePaymentReady");
            return false;
        }

        public int GetFCoin() {
            if (mFujiInstance != null)
                return mFujiInstance.Call<int>("getFCoin");
            return 0;
        }

        public UserInfo GetUserInfo() {
            if (mFujiInstance != null)
                return new UserInfo(mFujiInstance.Call<AndroidJavaObject>("getUserInfo"));

            return null;
        }

        public List<PaymentPackage> getPaymentPackages() {
            if (mFujiInstance != null) {
                AndroidJavaObject javaPaymentPackages = mFujiInstance.Call<AndroidJavaObject>("getPaymentPackages");
                var packages = new List<PaymentPackage>();

                for (int i = 0; i < javaPaymentPackages.Call<int>("size"); i++) {
                    packages.Add(new PaymentPackage(javaPaymentPackages.Call<AndroidJavaObject>("get", i)));
                }

                return packages;
            }

            return null;
        }

        public List<Transaction> getPendingTransactions() {
            if (mFujiInstance != null) {
                AndroidJavaObject javaPendingTransactions = mFujiInstance.Call<AndroidJavaObject>("getPendingTransactions");
                var transactions = new List<Transaction>();

                for (int i = 0; i < javaPendingTransactions.Call<int>("size"); i++) {
                    transactions.Add(new Transaction(javaPendingTransactions.Call<AndroidJavaObject>("get", i)));
                }

                return transactions;
            }

            return null;
        }

        public void getPaymentChannels() {
            if (mFujiInstance != null)
                mFujiInstance.Call("getPaymentChannels", new ProductDetailListener(EventPaymentChannelSucceed, EventPaymentChannelFailed));
        }

        public void Login() {
            if (mFujiInstance != null)
                mFujiInstance.Call("login", new LoginListener(EventLoginSucceed, EventLoginFailed, EventLoginCancelled));
        }

        public void Logout() {
            if (mFujiInstance != null)
                mFujiInstance.Call("logout");
        }

        public void ShowUserInfo() {
            if (mFujiInstance != null)
                mFujiInstance.Call("showUserInfo");
        }

        public void TransferCoin(string packageCode) {
            if (mFujiInstance != null) {
                mFujiInstance.Call("transferCoin", packageCode, new TransactionListener(EventTransferSucceed, EventTransferFailed, EventTransferCancelled));
            }
        }
    }
}
#endif