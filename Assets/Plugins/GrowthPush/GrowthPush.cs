using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class GrowthPush
{
	public enum Environment
	{
		unknow = 0,
		development,
		production
	};
	
	public enum Option
	{
		None = GrowthPushIOS.EGPOption.EGPOptionNone,
		TrackLaunch = GrowthPushIOS.EGPOption.EGPOptionTrackLaunch,
		TagDevie = GrowthPushIOS.EGPOption.EGPOptionTagDevie,
		TagOS = GrowthPushIOS.EGPOption.EGPOptionTagOS,
		TagLanguage = GrowthPushIOS.EGPOption.EGPOptionTagLanguage,
		TagTimeZone = GrowthPushIOS.EGPOption.EGPOptionTagTimeZone,
		TagVersion = GrowthPushIOS.EGPOption.EGPOptionTagVersion,
		TagBuild = GrowthPushIOS.EGPOption.EGPOptionTagBuild,
		TrackAll = GrowthPushIOS.EGPOption.EGPOptionTrackAll,
		TagAll = GrowthPushIOS.EGPOption.EGPOptionTagAll,
		All = GrowthPushIOS.EGPOption.EGPOptionAll,
	};
	
	public static void initialize(int applicationId, string secrect)
	{
		initialize(applicationId, secrect, Environment.development);
	}
	
	public static void initialize(int applicationId, string secrect, Environment environment)
	{
		initialize(applicationId, secrect, environment, Option.All);
	}
	
	public static void initialize(int applicationId, string secrect, Environment environment, Option option)
	{
		initialize(applicationId, secrect, environment, Option.All, true);
	}
	
	public static void initialize(int applicationId, string secrect, Environment evironment, Option option, bool debug)
	{
#if UNITY_ANDROID
		GrowthPushAndroid.Environment evAnd = GrowthPushAndroid.Environment.development;
		if(evironment == Environment.production)
			evAnd = GrowthPushAndroid.Environment.production;		
		GrowthPushAndroid.getInstance().initialize(applicationId, secrect, evAnd, debug); 
#elif UNITY_IPHONE
		GrowthPushIOS.setApplicationId(applicationId, secrect, (GrowthPushIOS.GPEnvironment)evironment, debug);
#endif
	}
	
	public static void register()
	{
		register(null);
	}
	
	public static void register(string senderId)
	{
#if UNITY_ANDROID
		GrowthPushAndroid.getInstance().register(senderId);
#elif UNITY_IPHONE
		GrowthPushIOS.requestDeviceToken(deviceToken => {
			if (deviceToken != null && deviceToken.Length != 0) 
			{
				GrowthPushIOS.setDeviceToken(deviceToken);
			}
		});
#endif
	}
	
	public static void trackEvent(string name)
	{
		trackEvent(name, "");
	}
	
	public static void trackEvent(string name, string val)
	{
#if UNITY_ANDROID
		GrowthPushAndroid.getInstance().trackEvent(name, val);
#elif UNITY_IPHONE
		GrowthPushIOS.trackEvent(name, val);
#endif
	}

	public static void setTag(string name)
	{
		setTag(name, "");
	}
	
	public static void setTag(string name, string val)
	{
#if UNITY_ANDROID
		GrowthPushAndroid.getInstance().setTag(name, val);
#elif UNITY_IPHONE
		GrowthPushIOS.setTag(name, val);
#endif
	}
	
	public static void requestDeviceToken()
	{
#if UNITY_IPHONE
		GrowthPushIOS.requestDeviceToken(null);
#endif
	}
	
  	public static void setDeviceToken(string deviceToken)
	{
#if UNITY_IPHONE
		GrowthPushIOS.setDeviceToken(deviceToken);
#endif
	}
	
  	public static void setDeviceTags()
	{
#if UNITY_ANDROID
		GrowthPushAndroid.getInstance().setDeviceTags();
#elif UNITY_IPHONE
		GrowthPushIOS.setDeviceTags();
#endif
	}
	
  	public static void clearBadge()
	{
#if UNITY_IPHONE
		GrowthPushIOS.clearBadge();
#endif
	}
	
	public static void onPushNotificationsReceived(Action<string> didPushNotificationsReceived)
	{
#if UNITY_IPHONE
		GrowthPushIOS.onPushNotificationsReceived(didPushNotificationsReceived);
#endif
	}
}
