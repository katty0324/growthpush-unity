using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class GrowthPushReceive : MonoBehaviour
{
	private static GameObject GO = null;
	public static GrowthPushReceive CreateGO()
	{
		GrowthPushReceive ret = null;
		if(GO == null)
		{
					
#if UNITY_IPHONE
			GO = new GameObject ("GrowthPushReceiveIOS");
			ret = GO.AddComponent<GrowthPushReceiveIOS>();
#elif UNITY_ANDROID
			GO = new GameObject ("GrowthPushReceiveAndroid");
			ret = GO.AddComponent<GrowthPushReceiveAndroid>();
#endif
			GameObject.DontDestroyOnLoad(GO);
		}
		else
		{
#if UNITY_IPHONE
			ret = GO.GetComponent<GrowthPushReceiveIOS>();
#elif UNITY_ANDROID
			ret = GO.GetComponent<GrowthPushReceiveAndroid>();
#endif
		}
		return ret;
	}	
	
	public Action<Dictionary<string, object>> launchWithNotificationCallback = null;
	public void launchWithNotification(string growthpushMsg)
	{
		if (launchWithNotificationCallback != null) 
		{
			Dictionary<string, object> obj = MiniJSON.Json.Deserialize(growthpushMsg) as Dictionary<string, object>;
			if(obj != null)
				launchWithNotificationCallback(obj);		
			else
				GrowthPush.trackEvent("Launch");
		}
	}
}

public class GrowthPushReceiveAndroid : GrowthPushReceive
{
}

public class GrowthPushReceiveIOS : GrowthPushReceive
{
	public Action<string> didRegisterForRemoteNotificationsWithDeviceTokenCallback = null;
	public void onDidRegisterForRemoteNotificationsWithDeviceToken(string deviceToken)
	{
		if (didRegisterForRemoteNotificationsWithDeviceTokenCallback != null) 
			didRegisterForRemoteNotificationsWithDeviceTokenCallback(deviceToken);			
	}
	
	//Sent when the application failed to be registered with Apple Push Notification Service (APNS).
	public void onDidFailToRegisterForRemoteNotificationsWithError(string error)
	{
		Debug.Log(error);
	}
}
	
	
