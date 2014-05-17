using UnityEngine;
using System.Collections;
using System;

public class GrowthPushAndroid
{
		private static GrowthPushAndroid instance = new GrowthPushAndroid();

		#if UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaObject growthPush;
		#endif

		private GrowthPushAndroid() {
				if (growthPush != null)
					return;
				#if UNITY_ANDROID && !UNITY_EDITOR
					using(AndroidJavaClass gpclass = new AndroidJavaClass( "com.growthpush.GrowthPush" )) {
							growthPush = gpclass.CallStatic<AndroidJavaObject>("getInstance"); 
					}
				#endif
		}

		public static Initialize(int applicationId, string secret, GrowthPush.Environment environment, bool debug) {
			instance.Initialize(applicationId, secret, environment, debug);
		}

		public static Register(string senderId) {
			instance.Register(applicationId, secret, environment, debug);
		}

		public void TrackEvent(string name, string val) {
			instance.TrackEvent(name, val);
		}

		public void SetTag(string name, string val) {
			instance.SetTag(name, val);
		}

		public void SetDeviceTags() {
			instance.SetDeviceTags();
		}

		public void Initialize(int applicationId, string secret, GrowthPush.Environment environment, bool debug, string senderId) {
				if (growthPush == null)
					return;
				#if UNITY_ANDROID && !UNITY_EDITOR
					AndroidJavaClass  environmentClass = new AndroidJavaClass("com.growthpush.model.Environment"); 
					AndroidJavaObject environmentObject = environmentClass.GetStatic<AndroidJavaObject>(environment == GrowthPush.Environment.Production ? "production" : "development"); 
					AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
					AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"); 
					growthPush.Call<AndroidJavaObject>("initialize", activity, applicationId, secret, environmentObject, debug);
					growthPush.Call<AndroidJavaObject>("register", senderId);
				#endif
		}

		public void TrackEvent(string name, string val) {
				if (growthPush == null)
					return;
				#if UNITY_ANDROID && !UNITY_EDITOR
					growthPush.Call("trackEvent", name, val);
				#endif
		}

		public void SetTag(string name, string val) {
				if (growthPush == null)
					return;
				#if UNITY_ANDROID && !UNITY_EDITOR
					growthPush.Call("setTag", name, val);
				#endif
		}

		public void SetDeviceTags() {
				if (growthPush == null)
					return;
				#if UNITY_ANDROID && !UNITY_EDITOR
					growthPush.Call("setDeviceTags");
				#endif
		}

		// TODO Refactor callback flow
		public void callTrackGrowthPushMessage() {
				#if UNITY_ANDROID && !UNITY_EDITOR
					using(AndroidJavaObject java = new AndroidJavaClass("com.growthpush.ExternalFramework")) {
						java.CallStatic("setFramework", "unity");
						java.CallStatic("callTrackGrowthPushMessage");
					}
				#endif
		}
}
