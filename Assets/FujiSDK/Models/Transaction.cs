using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
namespace Fuji {
    public class Transaction {
        private AndroidJavaObject obj;

        public Transaction(AndroidJavaObject obj) {
            this.obj = obj;
        }

        public int getFCoin() {
            return obj.Get<int>("fCoin");
        }

        public string getPaymentTelco() {
            return obj.Get<string>("paymentTelco");
        }

        public PaymentType getPaymentType() {
            var javaPaymentTypeClass = new AndroidJavaClass("com.fuji.fujisdk.payment.PaymentType");
            var javaPaymentTypeObj = obj.Get<AndroidJavaObject>("paymentType");

            if (javaPaymentTypeObj == javaPaymentTypeClass.GetStatic<AndroidJavaObject>("FCOIN"))
                return PaymentType.FCOIN;
            if (javaPaymentTypeObj == javaPaymentTypeClass.GetStatic<AndroidJavaObject>("TELCO"))
                return PaymentType.TELCO;
            if (javaPaymentTypeObj == javaPaymentTypeClass.GetStatic<AndroidJavaObject>("STORE"))
                return PaymentType.STORE;

            return PaymentType.STORE;
        }

        public string getPackageCode() {
            return obj.Get<string>("packageCode");
        }

        public string getOrderId() {
            return obj.Get<string>("orderId");
        }

        public string getPurchaseToken() {
            return obj.Get<string>("purchaseToken");
        }

        public string getPurchaseSignature() {
            return obj.Get<string>("purchaseSignature");
        }

        public int getPurchaseState() {
            return obj.Get<int>("purchaseState");
        }

        public long getPurchaseTime() {
            return obj.Get<long>("purchaseTime");
        }

        public string getPurchaseOriginalJson() {
            return obj.Get<string>("purchaseOriginalJson");
        }
    }
}
#endif