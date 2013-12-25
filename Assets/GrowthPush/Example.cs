using UnityEngine;
using System.Collections.Generic;

public class Example : MonoBehaviour {
	
	public int appID = 0;
	public string secret = "secret";
	public GrowthPush.Environment environment = GrowthPush.Environment.Development;
	public bool debug = true;
	public GrowthPush.Option option = GrowthPush.Option.All;
	public string senderID = "0000000000000";

	void Awake ()
	{
		GrowthPush.Initialize (appID, secret, environment, debug, option);
		GrowthPush.Register (senderID);
		GrowthPush.ClearBadge (); 
 
		GrowthPush.LaunchWithNotification (data => {
			object growthpushObj = null;
			if (data.TryGetValue ("growthpush", out growthpushObj)) {
				object notificationId = null;
				if ((growthpushObj as Dictionary<string, object>).TryGetValue ("notificationId", out notificationId)) {
					GrowthPush.TrackEvent ("Launch via push notification " + notificationId);
				}
			}
		});
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {	
		
	}
	
	void OnGUI ()
	{
		if (GUI.Button (new Rect (10, 10, 150, 100), "TrackEvent")) {
			GrowthPush.TrackEvent ("Launching");
		}
		if (GUI.Button (new Rect (10, 120, 150, 100), "SetTag")) {
			GrowthPush.SetTag ("Tagging");
		}
	}
}
