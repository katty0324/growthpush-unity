using UnityEngine;
using System.Collections;
using System;

public class ReceiveBehaviorAndroid : MonoBehaviour
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
	public static ReceiveBehaviorAndroid CreateGO()
	{
		ReceiveBehaviorAndroid ret = null;
		if(GO == null)
		{
			GO = new GameObject ("ReceiveHandlerAndroid");		
			ret = GO.AddComponent<ReceiveBehaviorAndroid>();
			GameObject.DontDestroyOnLoad(GO);
		}
		else
		{
			ret = GO.GetComponent<ReceiveBehaviorAndroid>();
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
	
	
