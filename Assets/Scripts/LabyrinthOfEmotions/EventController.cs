using System.Collections;
using System.Collections.Generic;


// temp test: code from http://www.willrmiller.com/?p=87
// to be changed... 

// SideNote: singleton implemented as per http://csharpindepth.com/Articles/General/Singleton.aspx#nested-cctor
public class EventController {
	private static readonly EventController instance = new EventController();
	
	// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
	static EventController() {
	}
	
	public static EventController Instance {
		get { return instance; }
	}
	
	// Use this for initialization
	private EventController () {
	}

	public delegate void EventDelegate<T> (T e) where T : GameEvent;
	private delegate void EventDelegate (GameEvent e);
	
	private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
	private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

	public void Subscribe<T> (EventDelegate<T> del) where T : GameEvent {	
		// Early-out if we've already registered this delegate
		if (delegateLookup.ContainsKey(del))
			return;
		
		// Create a new non-generic delegate which calls our generic one.
		// This is the delegate we actually invoke.
		EventDelegate internalDelegate = (e) => del((T)e);
		delegateLookup[del] = internalDelegate;
		
		EventDelegate tempDel;
		if (delegates.TryGetValue(typeof(T), out tempDel)) {
			delegates[typeof(T)] = tempDel += internalDelegate; 
		} else {
			delegates[typeof(T)] = internalDelegate;
		}
	}

	public void UnSubscribe<T> (EventDelegate<T> del) where T : GameEvent {
		EventDelegate internalDelegate;
		if (delegateLookup.TryGetValue(del, out internalDelegate)) {
			EventDelegate tempDel;
			if (delegates.TryGetValue(typeof(T), out tempDel)) {
				tempDel -= internalDelegate;
				if (tempDel == null) {
					delegates.Remove(typeof(T));
				} else {
					delegates[typeof(T)] = tempDel;
				}
			}
			
			delegateLookup.Remove(del);
		}
	}

	// TODO: Should add a cleanup method to remove all listeners on destroy !!
	public void OnApplicationQuit() {
		// TODO: see http://www.bendangelo.me/unity3d/2014/12/24/unity3d-event-manager.html
		// for remove all method etc !!
	}

	public void Publish (GameEvent e) {
		EventDelegate del;
		if (delegates.TryGetValue(e.GetType(), out del)) {
			del.Invoke(e);
		}
	}
}