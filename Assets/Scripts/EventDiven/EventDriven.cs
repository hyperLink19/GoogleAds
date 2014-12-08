using UnityEngine;
using System.Collections.Generic;
using System;

public static partial class G{
	public delegate void GEventHandler(GEvent e);
	
	class EventDriven{
		Dictionary<string, List<GEventHandler>> _handlers = new Dictionary<string, List<GEventHandler>>();
		
		public void RaiseEvent(GEvent e){
			List<GEventHandler> tmp;
			if (_handlers.TryGetValue(e.Name, out tmp))
			{
				GEventHandler[] handlers = tmp.ToArray(); // avoid modifying list while performing iterate
				foreach (var handler in handlers)
					handler.Invoke(e);
			}
		}
		
		public void AddEventHandler(string name, GEventHandler handler){
			List<GEventHandler> tmp;
			if (_handlers.TryGetValue(name, out tmp))
			{
				tmp.Add(handler);
			}
			else
			{
				_handlers[name] = new List<GEventHandler>();
				_handlers[name].Add(handler);
			}
		}

		public void RemoveEventHandler(string name, GEventHandler handler)
		{
			List<GEventHandler> tmp;
			if (_handlers.TryGetValue(name, out tmp))
			{
				tmp.Remove(handler);
			}
		}
	}
}