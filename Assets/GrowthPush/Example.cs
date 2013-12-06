using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {
	
	public int appID = 569;
	public string secrect = "99ym4ntccU89wj1SN3osYT8hqazrocKL";
	public bool debug = true;
	public GrowthPush.Environment environment = GrowthPush.Environment.development;
	public GrowthPush.Option option = GrowthPush.Option.All;
	public string senderID = "1078069531718";	

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {	
		
	}
	
	void OnGUI () {
		if (GUI.Button (new Rect (10,10,150,100), "Init GrowthPush")) {
			GrowthPush.initialize(appID, secrect, environment, option, debug);
			GrowthPush.register(senderID);
			GrowthPush.trackEvent("Launching");
			GrowthPush.setTag("Tagging");
			
			GrowthPush.onPushNotificationsReceived(pushMessage => {
				Debug.Log(pushMessage);
			});
		}
	}
}
