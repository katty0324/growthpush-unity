using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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
	
	
