using UnityEngine;

#if UNITY_ANDROID
namespace Fuji {
    class LoginListener : AndroidJavaProxy
    {
        private event VoidHandler eventSucceed;
        private event StringHandler eventFailed;
        private event VoidHandler eventCancelled;
        public LoginListener(VoidHandler eventSucceed, StringHandler eventFailed, VoidHandler eventCancelled) 
            : base("com.fuji.fujisdk.oauth.listener.LoginListener") {
            this.eventSucceed = eventSucceed;
            this.eventFailed = eventFailed;
            this.eventCancelled = eventCancelled;
        }
        void onLoginSucceed()
        {
            if (eventSucceed != null)
                eventSucceed.Invoke();
        }
        void onLoginFailed(string msg) {
            if (eventFailed != null)
                eventFailed.Invoke(msg);
        }
        void onLoginCancelled() {
            if (eventCancelled != null)
                eventCancelled.Invoke();
        }
    }
}
#endif