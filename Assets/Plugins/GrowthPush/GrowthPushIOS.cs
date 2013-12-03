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
	
	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_setApplicationId(int appID, string secrect, bool debug);
	[DllImport("__Internal")]
	private static extern void _easyGrowthPush_setApplicationId_option(int appID, string secrect, bool debug, int option);
		
	public static void setApplicationId(int appID, string secret, bool debug)
	{
		Debug.Log("_easyGrowthPush_setApplicationId");
#if !UNITY_EDITOR
		_easyGrowthPush_setApplicationId(appID, secret, debug);
#endif
	}
	
	public static void setApplicationId(int appID, string secret, bool debug, EGPOption option)
	{
		Debug.Log("_easyGrowthPush_setApplicationId_option");
#if !UNITY_EDITOR
		_easyGrowthPush_setApplicationId_option(appID, secret, debug, (int)option);
#endif
	}

};
#endif