using UnityEngine;
using System.Collections;

public class GrowthPushComponent : MonoBehaviour {

	void Awake () {
		GrowthPush.Initialize(1075, "IUblO1kYXwGRGBrXnXYwoOLN6ubKPjPe", GrowthPush.Environment.Development, true, "1000565500410");
		GrowthPush.TrackEvent("Launch");
		GrowthPush.SetDeviceTags();
		GrowthPush.ClearBadge();
	}

	void Start () {
		
	}

	void Update () {
	
	}
}
