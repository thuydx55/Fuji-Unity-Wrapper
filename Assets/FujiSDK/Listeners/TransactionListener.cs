using UnityEngine;

#if UNITY_ANDROID
namespace Fuji {
    class TransactionListener : AndroidJavaProxy
    {
        private event ObjectHandler<Transaction> eventSucceed;
        private event StringHandler eventFailed;
        private event VoidHandler eventCancelled;
        public TransactionListener(ObjectHandler<Transaction> eventSucceed, StringHandler eventFailed, VoidHandler eventCancelled) 
            : base("com.fuji.fujisdk.payment.listener.TransactionListener") {
            this.eventSucceed = eventSucceed;
            this.eventFailed = eventFailed;
            this.eventCancelled = eventCancelled;
        }
        void onSucceed(AndroidJavaObject obj)
        {
            if (eventSucceed != null)
                eventSucceed.Invoke(new Transaction(obj));
        }
        void onFailed(string msg) {
            if (eventFailed != null)
                eventFailed.Invoke(msg);
        }
        void onCancelled() {
            if (eventCancelled != null)
                eventCancelled.Invoke();
        }
    }
}
#endif