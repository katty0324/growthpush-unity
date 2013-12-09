using UnityEngine;
using System.Collections.Generic;

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
			
			GrowthPush.launchWithNotification(data => {
				Debug.Log("data " + data);
								
				string growthpushStr = null;
				if(data.TryGetValue("growthpush", out growthpushStr))
				{
					Debug.Log("growthpushStr " + growthpushStr);
					Dictionary<string, object> growthpushObj = MiniJSON.Json.Deserialize(growthpushStr) as Dictionary<string, object>;
					if(growthpushObj != null)
					{
						object notificationId;
						if(growthpushObj.TryGetValue("notificationId", out notificationId))
						{
							Debug.Log("unity notificationId: " + notificationId);
							GrowthPush.trackEvent("Launch via push notification " + notificationId);
						}
						else
							GrowthPush.trackEvent("Launch");
					}
					else
						GrowthPush.trackEvent("Launch");
				}
				GrowthPush.trackEvent("Launch");
			});
		}
	}
}
