using UnityEngine;
using System.Collections;

public static partial class G {
	static EventDriven _eventDriven;

	static GApp _app;
	
	public static void RaiseEvent(string name){
		_eventDriven.RaiseEvent(new GEvent(name));
	}
	
	public static void AddEventHandler(string name, GEventHandler handler){
		_eventDriven.AddEventHandler(name,handler);
	}

	public static void RemoveEventHandler(string name, GEventHandler handler){
		_eventDriven.RemoveEventHandler(name,handler);
	}

	static G(){
		_eventDriven = new EventDriven();
	}

	public static GApp App{
		get
		{
			return _app;
		}
		set
		{
			if (_app == null)
			{
				_app = value;
			}
			else
				throw new GException("App instance already created");
		}
	}
}
