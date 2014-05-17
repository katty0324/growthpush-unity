using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

public class GrowthPush {

		public enum Environment {
				Unknown = 0,
				Development = 1,
				Production = 2
		}

		public static void Initialize(int applicationId, string secret, Environment environment, bool debug) {
				#if UNITY_ANDROID	
					GrowthPushAndroid.Initialize(applicationId, secret, environment, debug);
				#elif UNITY_IPHONE
					GrowthPushIOS.Initialize(applicationId, secret, environment, debug); 
				#endif
		}

		public static void RequestDeviceToken() {
				#if UNITY_ANDROID
				#elif UNITY_IPHONE
					GrowthPushIOS.RequestDeviceToken();
				#endif
		}

		public static void RequestDeviceToken(string senderId) {
				#if UNITY_ANDROID
					GrowthPushAndroid.RequestDeviceToken(senderId);
				#elif UNITY_IPHONE
					RequestDeviceToken();
				#endif
		}

		public static void TrackEvent(string name) {
				TrackEvent(name, null);
		}

		public static void TrackEvent(string name, string val) {
				#if UNITY_ANDROID
					GrowthPushAndroid.TrackEvent(name, val);
				#elif UNITY_IPHONE
					GrowthPushIOS.TrackEvent(name, val);
				#endif
		}

		public static void SetTag(string name) {
				SetTag(name, null);
		}

		public static void SetTag (string name, string val) {
				#if UNITY_ANDROID
					GrowthPushAndroid.SetTag(name, val);
				#elif UNITY_IPHONE
					GrowthPushIOS.SetTag(name, val);
				#endif
		}

		public static void SetDeviceTags () {
				#if UNITY_ANDROID
					GrowthPushAndroid.SetDeviceTags();
				#elif UNITY_IPHONE
					GrowthPushIOS.SetDeviceTags();
				#endif
		}

		public static void ClearBadge () {
				#if UNITY_IPHONE
					GrowthPushIOS.ClearBadge();
				#endif
		}

		// TODO Refactor callback flow
		public static void LaunchWithNotification (Action<Dictionary<string, object>> callback) {
				GrowthPushReceive.getInstance ().setLaunchNotificationCallback (callback);
				#if UNITY_IPHONE
						GrowthPushIOS.callTrackGrowthPushMessage();
				#elif UNITY_ANDROID
						GrowthPushAndroid.GetInstance().callTrackGrowthPushMessage();
				#endif
		}

		public static void DefaultLaunchWithNotificationCallback (Dictionary<string, object> query) {
				if (query != null && query.ContainsKey ("growthpush")) {
						Dictionary<string, object> gpJson = query ["growthpush"] as Dictionary<string, object>;
						if (gpJson.ContainsKey ("notificationId")) {
								GrowthPush.TrackEvent ("Launch via push notification " + gpJson ["notificationId"]);
						}
				}
		}
}
