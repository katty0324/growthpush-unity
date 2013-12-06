using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GrowthPusReceiveAndroid : MonoBehaviour
{
	public Action<string> receiveCallback = null;
	public void onReceive(string str)
	{
		Debug.Log("ReceiveHandler receive " + str); 
		if(receiveCallback != null)
			receiveCallback(str);
	}
	
	public Action<string> openCallback = null;
	public void onOpen(string str)
	{
		Debug.Log("Callback open " + str);
		Dictionary<string, object> jsonObj = MiniJSON.Json.Deserialize(str) as Dictionary<string, object>;
		object notificationId = null;
		if(jsonObj != null)
		{
			jsonObj.TryGetValue("notificationId", out notificationId);
			Debug.Log("notificationId " + notificationId);
			if(notificationId != null && openCallback != null)
				openCallback(notificationId as string);
		}
	}
	
	private static GameObject GO = null;
	public static GrowthPusReceiveAndroid CreateGO()
	{
		GrowthPusReceiveAndroid ret = null;
		if(GO == null)
		{
			GO = new GameObject ("GrowthPusReceiveAndroid");		
			ret = GO.AddComponent<GrowthPusReceiveAndroid>();
			GameObject.DontDestroyOnLoad(GO);
		}
		else
		{
			ret = GO.GetComponent<GrowthPusReceiveAndroid>();
		}
		return ret;
	}	
	
	public static string ReceiveName
	{
		get{
			if(GO == null)
				return null;
			return GO.name;
		}
	}
}

public class GrowthPushReceiveIOS : MonoBehaviour
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
	
	public Action<string> didPushNotificationsReceivedCallback = null;
	public void onPushNotificationsReceived(string pushMessage)
	{
		Debug.Log(pushMessage);
		if(didPushNotificationsReceivedCallback != null)
			didPushNotificationsReceivedCallback(pushMessage);
	}
	
	public Action<string> didFinishLaunchWithNotificationIDCallback = null;
	public void didFinishLaunchWithNotificationID(string notificationId)
	{
		Debug.Log(notificationId);
		if(didFinishLaunchWithNotificationIDCallback != null)
			didFinishLaunchWithNotificationIDCallback(notificationId);
	}
	
	public void onDidBecomeActive(string str)
	{
		GrowthPushIOS.trackEvent("Launch");
	}
	
	private static GameObject GO = null;
	public static GrowthPushReceiveIOS CreateGO()
	{
		GrowthPushReceiveIOS ret = null;
		if(GO == null)
		{
			GO = new GameObject ("GrowthPushReceiveIOS");		
			ret = GO.AddComponent<GrowthPushReceiveIOS>();
			GameObject.DontDestroyOnLoad(GO);
		}
		else
		{
			ret = GO.GetComponent<GrowthPushReceiveIOS>();
		}
		return ret;
	}	
	
	public static string ReceiveName
	{
		get{
			if(GO == null)
				return null;
			return GO.name;
		}
	}
}
	
	
