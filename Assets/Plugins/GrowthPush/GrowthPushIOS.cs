using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class GrowthPushIOS {
	public enum EGPOption
	{
		EGPOptionNone = 0,
		EGPOptionTrackLaunch = 1 << 0,
		EGPOptionTagDevice = 1 << 1,
		EGPOptionTagOS = 1 << 2,
		EGPOptionTagLanguage = 1 << 3,
		EGPOptionTagTimeZone = 1 << 4,
		EGPOptionTagVersion = 1 << 5,
		EGPOptionTagBuild = 1 << 6,
		EGPOptionTrackAll = EGPOptionTrackLaunch,
		EGPOptionTagAll = EGPOptionTagDevice | EGPOptionTagOS | EGPOptionTagLanguage | EGPOptionTagTimeZone | EGPOptionTagVersion | EGPOptionTagBuild,
		EGPOptionAll = EGPOptionTrackAll | EGPOptionTagAll,
	};
	
	public enum GPEnvironment
	{
		GPEnvironmentUnknown = 0,
	    GPEnvironmentDevelopment,
	    GPEnvironmentProduction,
	};
	
#if UNITY_IPHONE	
	[DllImport("__Internal")]
	public static extern void callTrackGrowthPushMessage();
	
	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_setApplicationId(int applicationID, string secrect, int environment, bool debug, int option);
	
	[DllImport("__Internal")]
	private static extern void _growthPush_setApplicationId(int applicationID, string secrect, int environment, bool debug, int option);

	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_requestDeviceToken();

	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_setDeviceToken(string deviceToken);

	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_trackEvent(string name);

	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_trackEvent_value(string name, string val);

	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_setTag(string name);

	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_setTag_value(string name, string val);

	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_setDeviceTags();

	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_clearBadge();
#endif
	
	public static void SetApplicationId(int applicationID, string secrect, GrowthPush.Environment environment, bool debug, GrowthPush.Option option)
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		_easyGrowthPush_setApplicationId(applicationID, secrect, (int)environment, debug, (int)option);
#endif
	}
	
	public static void RequestDeviceToken(Action<string> didRequestDeviceToken)
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		GrowthPushReceiveIOS receive = GrowthPushReceive.CreateGO() as GrowthPushReceiveIOS;
		if(receive != null)
			receive.DidRegisterForRemoteNotificationsWithDeviceTokenCallback = didRequestDeviceToken;
		
		_easyGrowthPush_requestDeviceToken();
#endif
	}
	
	public static void SetDeviceToken(string deviceToken)
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		_easyGrowthPush_setDeviceToken(deviceToken);
#endif
	}
	
	
	public static void TrackEvent(string name)
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		_easyGrowthPush_trackEvent(name);
#endif
	}
	
	public static void TrackEvent(string name, string val)
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		_easyGrowthPush_trackEvent_value(name, val);
#endif
	}
	
	public static void SetTag(string name)
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		_easyGrowthPush_setTag(name);
#endif
	}
	
	public static void SetTag(string name, string val)
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		_easyGrowthPush_setTag_value(name, val);
#endif
	}
	
	public static void SetDeviceTags()
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		_easyGrowthPush_setDeviceTags();
#endif
	}
	
	public static void ClearBadge()
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		_easyGrowthPush_clearBadge();
#endif
	}

};
