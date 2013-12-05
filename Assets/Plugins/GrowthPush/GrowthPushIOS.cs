using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
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
	extern static public void setListenerGameObject(string listenerName);
	
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
		_growthPush_setApplicationId(appID, secrect, (int)environment, debug, (int)EGPOption.EGPOptionAll);
	}
	
	public static void setApplicationId(int appID, string secrect, GPEnvironment environment, bool debug, EGPOption option)
	{
		_growthPush_setApplicationId(appID, secrect, (int)environment, debug, (int)option);
	}
	
	public static void requestDeviceToken()
	{
		_growthPush_requestDeviceToken();
	}
	
	public static void setDeviceToken(string deviceToken)
	{
		_growthPush_setDeviceToken(deviceToken);
	}
	
	
	public static void trackEvent(string name)
	{
		_growthPush_trackEvent(name);
	}
	
	public static void trackEvent(string name, string val)
	{
		_growthPush_trackEvent_value(name, val);
	}
	
	public static void setTag(string name)
	{
		_growthPush_setTag(name);
	}
	
	public static void setTag(string name, string val)
	{
		_growthPush_setTag_value(name, val);
	}
	
	public static void setDeviceTags()
	{
		_growthPush_setDeviceTags();
	}
	
	public static void clearBadge()
	{
		_growthPush_clearBadge();
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