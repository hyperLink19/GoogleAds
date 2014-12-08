using UnityEngine;
using System.Collections;

public sealed class GApp : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		if(G.App == null){
			G.App = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}else
			Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Start () {
		G.RaiseEvent(BaseEvent.AppStart);
		G.RaiseEvent(BaseEvent.AppStarted);
	}

	void OnApplicationPause(bool pause){
		if(pause)
			G.RaiseEvent(BaseEvent.UserPaused);
		else
			G.RaiseEvent(BaseEvent.UserUnPaused);
	}
}
