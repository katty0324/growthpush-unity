using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
#if UNITY_IPHONE
public class GrowthPushIOS {
	public enum EGPOption
	{
		EGPOptionNone = 0,
		EGPOptionTrackLaunch = 1 << 0,
		EGPOptionTagDevie = 1 << 1,
		EGPOptionTagOS = 1 << 2,
		EGPOptionTagLanguage = 1 << 3,
		EGPOptionTagTimeZone = 1 << 4,
		EGPOptionTagVersion = 1 << 5,
		EGPOptionTagBuild = 1 << 6,
		EGPOptionTrackAll = EGPOptionTrackLaunch,
		EGPOptionTagAll = EGPOptionTagDevie | EGPOptionTagOS | EGPOptionTagLanguage | EGPOptionTagTimeZone | EGPOptionTagVersion | EGPOptionTagBuild,
		EGPOptionAll = EGPOptionTrackAll | EGPOptionTagAll,
	};
	
	public enum GPEnvironment
	{
		GPEnvironmentUnknown = 0,
	    GPEnvironmentDevelopment,
	    GPEnvironmentProduction,
	};
	
	[DllImport("__Internal")]
	extern static private void setListenerGameObject(string listenerName);
	
	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_setApplicationId(int appID, string secrect, bool debug);
	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_setApplicationId_option(int appID, string secrect, bool debug, int option);
	
	[DllImport("__Internal")]
	private static extern void _growthPush_setApplicationId(int appID, string secrect, int environment, bool debug, int option);

	[DllImport("__Internal")]
	private static extern void _growthPush_requestDeviceToken();

	[DllImport("__Internal")]
	private static extern void _growthPush_setDeviceToken(string deviceToken);

	[DllImport("__Internal")]
	private static extern void _growthPush_trackEvent(string name);

	[DllImport("__Internal")]
	private static extern void _growthPush_trackEvent_value(string name, string val);

	[DllImport("__Internal")]
	private static extern void _growthPush_setTag(string name);

	[DllImport("__Internal")]
	private static extern void _growthPush_setTag_value(string name, string val);

	[DllImport("__Internal")]
	private static extern void _growthPush_setDeviceTags();

	[DllImport("__Internal")]
	private static extern void _growthPush_clearBadge();
	
	public static void setApplicationId(int appID, string secrect, GPEnvironment environment, bool debug)
	{
#if !UNITY_EDITOR
		_growthPush_setApplicationId(appID, secrect, (int)environment, debug, (int)EGPOption.EGPOptionAll);
#endif
	}
	
	public static void setApplicationId(int appID, string secrect, GPEnvironment environment, bool debug, EGPOption option)
	{
#if !UNITY_EDITOR
		_growthPush_setApplicationId(appID, secrect, (int)environment, debug, (int)option);
#endif
	}
	
	public static void requestDeviceToken(Action<string> didRequestDeviceToken)
	{
#if !UNITY_EDITOR
		GrowthPushReceiveIOS receive = GrowthPushReceiveIOS.CreateGO();
		if(receive != null)
			receive.didRegisterForRemoteNotificationsWithDeviceTokenCallback = didRequestDeviceToken;
		
		setListenerGameObject(GrowthPushReceiveIOS.ReceiveName);
		_growthPush_requestDeviceToken();
#endif
	}
	
	public static void setDeviceToken(string deviceToken)
	{
#if !UNITY_EDITOR
		_growthPush_setDeviceToken(deviceToken);
#endif
	}
	
	
	public static void trackEvent(string name)
	{
#if !UNITY_EDITOR
		_growthPush_trackEvent(name);
#endif
	}
	
	public static void trackEvent(string name, string val)
	{
#if !UNITY_EDITOR
		_growthPush_trackEvent_value(name, val);
#endif
	}
	
	public static void setTag(string name)
	{
#if !UNITY_EDITOR
		_growthPush_setTag(name);
#endif
	}
	
	public static void setTag(string name, string val)
	{
#if !UNITY_EDITOR
		_growthPush_setTag_value(name, val);
#endif
	}
	
	public static void setDeviceTags()
	{
#if !UNITY_EDITOR
		_growthPush_setDeviceTags();
#endif
	}
	
	public static void clearBadge()
	{
#if !UNITY_EDITOR
		_growthPush_clearBadge();
#endif
	}
		
	public static void EasySetApplicationId(int appID, string secret, bool debug)
	{
		Debug.Log("_easyGrowthPush_setApplicationId");
#if !UNITY_EDITOR
		_easyGrowthPush_setApplicationId(appID, secret, debug);
#endif
	}
	
	public static void EasySetApplicationId(int appID, string secret, bool debug, EGPOption option)
	{
		Debug.Log("_easyGrowthPush_setApplicationId_option");
#if !UNITY_EDITOR
		_easyGrowthPush_setApplicationId_option(appID, secret, debug, (int)option);
#endif
	}

};
#endif