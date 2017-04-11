using UnityEngine;
using System.Collections.Generic;

#if UNITY_ANDROID
namespace Fuji {
    class ProductDetailListener : AndroidJavaProxy
    {
        private event ObjectHandler<List<string> > eventSucceed;
        private event StringHandler eventFailed;
        public ProductDetailListener(ObjectHandler<List<string> > eventSucceed, StringHandler eventFailed) 
            : base("com.fuji.fujisdk.payment.listener.ProductDetailListener") {
            this.eventSucceed = eventSucceed;
            this.eventFailed = eventFailed;
        }
        void onLoginSucceed(AndroidJavaObject obj)
        {
            if (eventSucceed != null) {
                var products = new List<string>();

                for (int i = 0; i < obj.Call<int>("size"); i++) {
                    products.Add(obj.Call<string>("get", i));
                }

                eventSucceed.Invoke(products);
            }
        }
        void onLoginFailed(string msg) {
            if (eventFailed != null)
                eventFailed.Invoke(msg);
        }
    }
}
#endif