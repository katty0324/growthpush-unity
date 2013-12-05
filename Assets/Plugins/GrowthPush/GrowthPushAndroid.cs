using UnityEngine;
using System.Collections;
using System;


#if UNITY_ANDROID

public class GrowthPushAndroid
{
	public enum Environment 
	{
		production, 
		development
	};
	
	private static GrowthPushAndroid instance = null;	
	
	public static GrowthPushAndroid getInstance() 
	{
		if(instance == null)
			instance = new GrowthPushAndroid();
		return instance;
	}
	
	private AndroidJavaObject growthPush = null;	
	
	private GrowthPushAndroid()
	{
		using(AndroidJavaClass gpclass = new AndroidJavaClass( "com.growthpush.GrowthPush" ))
		{
			growthPush = gpclass.CallStatic<AndroidJavaObject>("getInstance");
		}
	}

	public GrowthPushAndroid initialize(int applicationId, string secret) 
	{
		return initialize(applicationId, secret, Environment.production, false);
	}

	public GrowthPushAndroid initialize(int applicationId, string secret, Environment environment) 
	{
		return initialize(applicationId, secret, environment, false);
	}

	public GrowthPushAndroid initialize(int applicationId, string secret, Environment environment, bool debug) 
	{		
		if( growthPush != null )
		{
			AndroidJavaClass enviClassJava = new AndroidJavaClass("com.growthpush.model.Environment");
			AndroidJavaObject enviObjJava = enviClassJava.GetStatic<AndroidJavaObject>(environment == Environment.production ? "production" : "development");
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        	AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			growthPush.Call<AndroidJavaObject>("initialize", activity, applicationId, secret, enviObjJava, debug);
		}
		else
		{
			Debug.LogError( "growthPush is not created.");
		}
		return this;
	}
	
	public GrowthPushAndroid register(string senderId) 
	{
		if( growthPush != null )
		{
			growthPush.Call<AndroidJavaObject>("register", senderId);
		}
		else
		{
			Debug.LogError( "growthPush is not created.");
		}
		return this;
	}
	
	public void trackEvent(string name) 
	{
		trackEvent(name, "");
	}
	
	public void trackEvent(string name, string val) 
	{
		if( growthPush != null )
		{
			growthPush.Call("trackEvent", name, val);
		}
		else
		{
			Debug.LogError( "growthPush is not created.");
		}
	}
	
	public void setTag(string name) 
	{
		setTag(name, "");
	}
		
	public void setTag(string name, string val) 
	{
		if( growthPush != null )
		{
			growthPush.Call("setTag", name, val);
		}
		else
		{
			Debug.LogError( "growthPush is not created.");
		}
	}
		
	public void setDeviceTags() 
	{
		if( growthPush != null )
		{
			growthPush.Call("setDeviceTags");
		}
		else
		{
			Debug.LogError( "growthPush is not created.");
		}
	}
	
	public void setReceiveHandler(ReceiveHandlerAndroid handler)
	{
		if( growthPush != null )
		{
			growthPush.Call("setReceiveHandler", handler.receiveJava);
		}
		else
		{
			Debug.LogError( "growthPush is not created.");
		}
	}
}

public class ReceiveHandlerAndroid
{
	public AndroidJavaObject receiveJava = null;	
	public ReceiveHandlerAndroid(Action<string> callback)
	{
		ReceiveBehaviorAndroid behavior =  ReceiveBehaviorAndroid.CreateGO();
		if(behavior != null)
			behavior.receiveCallback = callback;
		receiveJava = new AndroidJavaObject( "com.growthpush.handler.UnityReceiveHandler", ReceiveBehaviorAndroid.ReceiveName );
	}	
	
	public void setCallback(CallbackAndroid inCallback)
	{
		if(receiveJava != null)
		{
			receiveJava.Call("setCallback", inCallback.callbackJava);
		}
	}
		
}

public class CallbackAndroid
{	
	public AndroidJavaObject callbackJava = null;
	public CallbackAndroid(Action<string> callback)
	{
		ReceiveBehaviorAndroid behavior = ReceiveBehaviorAndroid.CreateGO();
		if(behavior != null)
			behavior.openCallback = callback;
		callbackJava = new AndroidJavaObject( "com.growthpush.handler.UnityCallback", ReceiveBehaviorAndroid.ReceiveName );
	}	
};

#endif

