using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
namespace Fuji {
    public class PaymentPackage {
        private AndroidJavaObject obj;

        public PaymentPackage(AndroidJavaObject obj) {
            this.obj = obj;
        }

        public string getPackageCode() {
            return obj.Get<string>("packageCode");
        }

        public string getPackageId() {
            return obj.Get<string>("packageId");
        }

        public string getPackageName() {
            return obj.Get<string>("packageName");
        }

        public string getPackageTelco() {
            return obj.Get<string>("packageTelco");
        }

        public int getFCoin() {
            return obj.Get<int>("fCoin");
        }
    }
}
#endif