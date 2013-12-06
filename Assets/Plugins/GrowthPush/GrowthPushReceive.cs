using UnityEngine;
using System.Collections;
using System;

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
		if(openCallback != null)
			openCallback(str);
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
	
	
