using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class GrowthPushReceive : MonoBehaviour
{
	private static GameObject GO = null;
	public static GrowthPushReceive CreateGO()
	{
		GrowthPushReceive receive = null;
		if(GO == null)
		{
					
#if UNITY_IPHONE
			GO = new GameObject ("GrowthPushReceiveIOS");
			receive = GO.AddComponent<GrowthPushReceiveIOS>();
#elif UNITY_ANDROID
			GO = new GameObject ("GrowthPushReceiveAndroid");
			receive = GO.AddComponent<GrowthPushReceiveAndroid>();
#endif
			GameObject.DontDestroyOnLoad(GO);
		}
		else
		{
#if UNITY_IPHONE
			receive = GO.GetComponent<GrowthPushReceiveIOS>();
#elif UNITY_ANDROID
			receive = GO.GetComponent<GrowthPushReceiveAndroid>();
#endif
		}
		return receive;
	}	
	
	public Action<Dictionary<string, object>> LaunchWithNotificationCallback = null;
	public void LaunchWithNotification(string query)
	{
		Debug.Log("query " + query);
		if (LaunchWithNotificationCallback != null && query != null) 
		{
			Dictionary<string, object> obj = null;
#if UNITY_ANDROID
			Dictionary<string, string> temp = query.Split('&').Select(p => p.Split('=')).ToDictionary(p => p[0], p => p.Length > 1 ? p[1] : null);
			foreach (KeyValuePair<string, string> pair in temp)
			{
				if(obj == null)
					obj = new Dictionary<string, object>();
				obj.Add(pair.Key, MiniJSON.Json.Deserialize(pair.Value));
			}
#elif UNITY_IPHONE
			obj = MiniJSON.Json.Deserialize(query) as Dictionary<string, object>;
#endif
			if(obj != null || obj.Count > 0)
				LaunchWithNotificationCallback(obj);		
		}
	}
}

public class GrowthPushReceiveAndroid : GrowthPushReceive
{
}

public class GrowthPushReceiveIOS : GrowthPushReceive
{
	public Action<string> DidRegisterForRemoteNotificationsWithDeviceTokenCallback = null;
	public void OnDidRegisterForRemoteNotificationsWithDeviceToken(string deviceToken)
	{
		if (DidRegisterForRemoteNotificationsWithDeviceTokenCallback != null) 
			DidRegisterForRemoteNotificationsWithDeviceTokenCallback(deviceToken);			
	}
	
	//Sent when the application failed to be registered with Apple Push Notification Service (APNS).
	public void OnDidFailToRegisterForRemoteNotificationsWithError(string error)
	{
		Debug.Log(error);
	}
}
	
	
