using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class GrowthPushIOS
{
		#if UNITY_IPHONE
			[DllImport("__Internal")]
			private static extern void _easyGrowthPush_setApplicationId(int applicationID, string secrect, int environment, bool debug, int option);

			[DllImport("__Internal")] 
			private static extern void _easyGrowthPush_trackEvent_value(string name, string val);

			[DllImport("__Internal")]
			private static extern void _easyGrowthPush_setTag_value(string name, string val);

			[DllImport("__Internal")]
			private static extern void _easyGrowthPush_setDeviceTags();

			[DllImport("__Internal")]
			private static extern void _easyGrowthPush_clearBadge();

			[DllImport("__Internal")]
			private static extern void _easyGrowthPush_requestDeviceToken();

			[DllImport("__Internal")]
			private static extern void _easyGrowthPush_setDeviceToken(string deviceToken);

			// TODO Refactor callback flow
			[DllImport("__Internal")]
			public static extern void callTrackGrowthPushMessage();
		#endif

		public static void Initialize(int applicationID, string secrect, GrowthPush.Environment environment, bool debug) {
				#if UNITY_IPHONE && !UNITY_EDITOR
					_easyGrowthPush_setApplicationId(applicationID, secrect, (int)environment, debug;
				#endif
		}

		public static void TrackEvent(string name, string val) {
				#if UNITY_IPHONE && !UNITY_EDITOR
					_easyGrowthPush_trackEvent_value(name, val);
				#endif
		}

		public static void SetTag(string name, string val) {
				#if UNITY_IPHONE && !UNITY_EDITOR
					_easyGrowthPush_setTag_value(name, val);
				#endif
		}

		public static void SetDeviceTags() {
				#if UNITY_IPHONE && !UNITY_EDITOR
					_easyGrowthPush_setDeviceTags();
				#endif
		}

		public static void ClearBadge() {
				#if UNITY_IPHONE && !UNITY_EDITOR
					_easyGrowthPush_clearBadge();
				#endif
		}

		// TODO Check if the following methods are needed.
		public static void RequestDeviceToken (Action<string> didRequestDeviceToken) {
				#if UNITY_IPHONE && !UNITY_EDITOR
					GrowthPushReceiveIOS receive = GrowthPushReceive.getInstance () as GrowthPushReceiveIOS;
					if(receive != null)
						receive.DidRegisterForRemoteNotificationsWithDeviceTokenCallback = didRequestDeviceToken;
					
					_easyGrowthPush_requestDeviceToken();
				#endif
		}

		public static void SetDeviceToken(string deviceToken) {
				#if UNITY_IPHONE && !UNITY_EDITOR
					_easyGrowthPush_setDeviceToken(deviceToken);
				#endif
		}

};
