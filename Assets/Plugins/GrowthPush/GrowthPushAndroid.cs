using UnityEngine;
using System.Collections;
using System;

public class GrowthPushAndroid
{
		private static GrowthPushAndroid instance = new GrowthPushAndroid ();
		private static AndroidJavaObject growthPush;

		public GrowthPushAndroid ()
		{
				#if UNITY_ANDROID && !UNITY_EDITOR
				if (growthPush == null) {
				using(AndroidJavaClass gpclass = new AndroidJavaClass( "com.growthpush.GrowthPush" ))
				{
				growthPush = gpclass.CallStatic<AndroidJavaObject>("getInstance"); 
				}
				}
				#endif
		}

		public static GrowthPushAndroid GetInstance ()
		{
				return instance;
		}

		public GrowthPushAndroid Initialize (int applicationId, string secret, GrowthPush.Environment environment, bool debug)
		{		
				#if UNITY_ANDROID && !UNITY_EDITOR
				if(growthPush != null)
				{
				AndroidJavaClass  environmentClass = new AndroidJavaClass("com.growthpush.model.Environment"); 
				AndroidJavaObject environmentObject = environmentClass.GetStatic<AndroidJavaObject>(environment == GrowthPush.Environment.Production ? "production" : "development"); 
				AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
				AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"); 
				growthPush.Call<AndroidJavaObject>("initialize", activity, applicationId, secret, environmentObject, debug);
				}
				#endif
				return this;
		}

		public GrowthPushAndroid Register (string senderId)
		{
				#if UNITY_ANDROID && !UNITY_EDITOR
						if( growthPush != null )
							growthPush.Call<AndroidJavaObject>("register", senderId);
				#endif
				return this;
		}

		public void TrackEvent (string name)
		{
				TrackEvent (name, "");
		}

		public void TrackEvent (string name, string val)
		{
				#if UNITY_ANDROID && !UNITY_EDITOR
					if( growthPush != null )
						growthPush.Call("trackEvent", name, val);
				#endif
		}

		public void SetTag (string name)
		{
				SetTag (name, "");
		}

		public void SetTag (string name, string val)
		{
				#if UNITY_ANDROID && !UNITY_EDITOR
					if( growthPush != null )
						growthPush.Call("setTag", name, val);
				#endif
		}

		public void SetDeviceTags ()
		{
				#if UNITY_ANDROID && !UNITY_EDITOR
					if( growthPush != null )
						growthPush.Call("setDeviceTags");
				#endif
		}

		public void callTrackGrowthPushMessage ()
		{
				#if UNITY_ANDROID && !UNITY_EDITOR
					using(AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
					{
			    		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
						if(activity != null)
							activity.CallStatic("callTrackGrowthPushMessage");
					}
				#endif
		}
}
