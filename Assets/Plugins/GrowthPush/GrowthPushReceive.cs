using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class GrowthPushReceive : MonoBehaviour
{
		private static GrowthPushReceive instance;
		private Action<Dictionary<string, object>> LaunchWithNotificationCallback = null;

		public static GrowthPushReceive getInstance ()
		{
				if (instance == null) {
						GameObject GO = null;
						#if UNITY_IPHONE
							GO = new GameObject ("GrowthPushReceiveIOS");
							instance = GO.AddComponent<GrowthPushReceiveIOS>();
						#elif UNITY_ANDROID
							GO = new GameObject ("GrowthPushReceiveAndroid");
							instance = GO.AddComponent<GrowthPushReceiveAndroid>();
						#endif

						if (GO != null)
								GameObject.DontDestroyOnLoad (GO);
				}

				return instance;
		}

		public void LaunchWithNotification (string query)
		{
				if (LaunchWithNotificationCallback != null && query != null) {
						Dictionary<string, object> obj = MiniJSON.Json.Deserialize (query) as Dictionary<string, object>;
						if (obj != null || obj.Count > 0)
								LaunchWithNotificationCallback (obj);		
				}
		}

		public void setLaunchNotificationCallback(Action<Dictionary<string, object>> callback) 
		{
				this.LaunchWithNotificationCallback = callback;
		}
}

public class GrowthPushReceiveAndroid : GrowthPushReceive
{
}

public class GrowthPushReceiveIOS : GrowthPushReceive
{
		public Action<string> DidRegisterForRemoteNotificationsWithDeviceTokenCallback = null;

		public void OnDidRegisterForRemoteNotificationsWithDeviceToken (string deviceToken)
		{
				if (DidRegisterForRemoteNotificationsWithDeviceTokenCallback != null)
						DidRegisterForRemoteNotificationsWithDeviceTokenCallback (deviceToken);			
		}
		//Sent when the application failed to be registered with Apple Push Notification Service (APNS).
		public void OnDidFailToRegisterForRemoteNotificationsWithError (string error)
		{
				Debug.Log (error);
		}
}
	
	
