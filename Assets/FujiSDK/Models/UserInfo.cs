using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
namespace Fuji {
    public class UserInfo {
        private AndroidJavaObject obj;

        public UserInfo(AndroidJavaObject obj) {
            this.obj = obj;
        }

        public string getId() {
            return obj.Get<string>("id");
        }

        public string getFullname() {
            return obj.Get<string>("fullName");
        }

        public string getEmail() {
            return obj.Get<string>("email");
        }

        public string getPhonenumber() {
            return obj.Get<string>("phoneNumber");
        }

        public string getBirthday() {
            return obj.Get<string>("birthDay");
        }

        public int getFCoin() {
            return obj.Get<int>("fCoin");
        }

        public string getCardId() {
            return obj.Get<string>("cardId");
        }

        public string getCardDate() {
            return obj.Get<string>("cardDate");
        }
    }
}
#endif