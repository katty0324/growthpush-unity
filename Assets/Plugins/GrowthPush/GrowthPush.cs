using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

public class GrowthPush
{
		public enum Environment
		{
				Unknown,
				Development,
				Production
		}

		public enum Option
		{
				None = 0,
				TrackLaunch = 1 << 0,
				TagDevice = 1 << 1,
				TagOS = 1 << 2,
				TagLanguage = 1 << 3,
				TagTimeZone = 1 << 4,
				TagVersion = 1 << 5,
				TagBuild = 1 << 6,
				TrackAll = TrackLaunch,
				TagAll = TagDevice | TagOS | TagLanguage | TagTimeZone | TagVersion | TagBuild,
				All = TrackAll | TagAll,
		};

		public static void Initialize (int applicationId, string secret)
		{
				Initialize (applicationId, secret, Environment.Production);
		}

		public static void Initialize (int applicationId, string secret, Environment environment)
		{
				Initialize (applicationId, secret, environment, false);
		}

		public static void Initialize (int applicationId, string secret, Environment environment, bool debug)
		{
				Initialize (applicationId, secret, environment, debug, Option.All);
		}

		public static void Initialize (int applicationId, string secret, Environment environment, bool debug, Option option)
		{
			#if UNITY_ANDROID	
				GrowthPushAndroid.GetInstance().Initialize(applicationId, secret, environment, debug);
			#elif UNITY_IPHONE
				GrowthPushIOS.SetApplicationId(applicationId, secret, environment, debug, option); 
			#endif
		}

		public static void Register ()
		{
				Register ("");
		}

		public static void Register (string senderId)
		{
			#if UNITY_ANDROID
					GrowthPushAndroid.GetInstance().Register(senderId);
			#elif UNITY_IPHONE
					GrowthPushIOS.RequestDeviceToken(deviceToken => {
						if (deviceToken != null && deviceToken.Length != 0) 
						{
							GrowthPushIOS.SetDeviceToken(deviceToken);
						}
					});
			#endif
		}

		public static void TrackEvent (string name)
		{
				TrackEvent (name, "");
		}

		public static void TrackEvent (string name, string val)
		{
			#if UNITY_ANDROID
					GrowthPushAndroid.GetInstance().TrackEvent(name, val);
			#elif UNITY_IPHONE
					GrowthPushIOS.TrackEvent(name, val);
			#endif
		}

		public static void SetTag (string name)
		{
				SetTag (name, "");
		}

		public static void SetTag (string name, string val)
		{
			#if UNITY_ANDROID
					GrowthPushAndroid.GetInstance().SetTag(name, val);
			#elif UNITY_IPHONE
					GrowthPushIOS.SetTag(name, val);
			#endif
		}

		public static void RequestDeviceToken ()
		{
			#if UNITY_IPHONE
					GrowthPushIOS.RequestDeviceToken(null);
			#endif
		}

		public static void SetDeviceToken (string deviceToken)
		{
			#if UNITY_IPHONE
					GrowthPushIOS.SetDeviceToken(deviceToken);
			#endif
		}

		public static void SetDeviceTags ()
		{
			#if UNITY_ANDROID
					GrowthPushAndroid.GetInstance().SetDeviceTags();
			#elif UNITY_IPHONE
					GrowthPushIOS.SetDeviceTags();
			#endif
		}

		public static void ClearBadge ()
		{
			#if UNITY_IPHONE
					GrowthPushIOS.ClearBadge();
			#endif
		}

		public static void LaunchWithNotification (Action<Dictionary<string, object>> callback)
		{
				GrowthPushReceive.getInstance ().setLaunchNotificationCallback (callback);
				#if UNITY_IPHONE
						GrowthPushIOS.callTrackGrowthPushMessage();
				#elif UNITY_ANDROID
						GrowthPushAndroid.GetInstance().callTrackGrowthPushMessage();
				#endif
		}

		public static void DefaultLaunchWithNotificationCallback (Dictionary<string, object> query)
		{
				if (query != null && query.ContainsKey ("growthpush")) {
						Dictionary<string, object> gpJson = query ["growthpush"] as Dictionary<string, object>;
						if (gpJson.ContainsKey ("notificationId")) {
								GrowthPush.TrackEvent ("Launch via push notification " + gpJson ["notificationId"]);
						}
				}
		}
}
