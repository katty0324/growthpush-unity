using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {
	
	public int appID = 569;
	public string secrect = "99ym4ntccU89wj1SN3osYT8hqazrocKL";
	public bool debug = true;
#if UNITY_IPHONE
	public GrowthPushIOS.GPEnvironment environment = GrowthPushIOS.GPEnvironment.GPEnvironmentDevelopment;
	public GrowthPushIOS.EGPOption option = GrowthPushIOS.EGPOption.EGPOptionAll;
#elif UNITY_ANDROID
	public GrowthPushAndroid.Environment environment = GrowthPushAndroid.Environment.development;
	public string senderID = "1078069531718";
#endif
	

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {	
		
	}
	
	void OnGUI () {
		if (GUI.Button (new Rect (10,10,150,100), "Init GrowthPush")) {
#if UNITY_IPHONE
			GrowthPushIOS.setApplicationId(appID, secrect, environment, debug, option); 
			GrowthPushIOS.requestDeviceToken(deviceToken => {
				if (deviceToken != null && deviceToken.Length!=0) 
				{
					GrowthPushIOS.setDeviceToken(deviceToken);
				}
			});			
			GrowthPushIOS.trackEvent("IOS Launch 123");
			GrowthPushIOS.setTag("IOS Tag 123");
#elif UNITY_ANDROID
			GrowthPushAndroid.getInstance().initialize(appID, secrect, environment, debug);
			GrowthPushAndroid.getInstance().register(senderID);
			
			ReceiveHandlerAndroid receiveHandler = new ReceiveHandlerAndroid(str => {
				Debug.Log("ReceiveHandlerAndroid receive " + str);
			});
			
			receiveHandler.setCallback(new CallbackAndroid(str => {
				Debug.Log("CallbackAndroid open " + str);
				
			}));
				
			GrowthPushAndroid.getInstance().setReceiveHandler(receiveHandler);
			GrowthPushAndroid.getInstance().trackEvent("Android Launch 123");
			GrowthPushAndroid.getInstance().setTag("Android Tag 123");
#endif
		}
	}
}
