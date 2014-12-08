using System;
using System.Collections;

public class GEvent {

	private string _name;
	private object _args;
	private object _sender;

	public GEvent(string name){
		_name = name;
	}

	public GEvent(string name, object args, object sender){
		_name = name;
		_args = args;
		_sender = sender;
	}

	public string Name { get { return _name; } }
	public object Args { get { return _args; } }
	public object Sender { get { return _sender; } }
}
