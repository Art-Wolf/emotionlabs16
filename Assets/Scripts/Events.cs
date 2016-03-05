using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEvent
{
}

public class GoWestEvent : GameEvent {
	public string someVar { get; private set; }

	public GoWestEvent(string someVar){
		this.someVar = someVar;
	}
}

public class GoNorthEvent : GameEvent {
	public string someVar { get; private set; }

	public GoNorthEvent(string someVar){
		this.someVar = someVar;
	}
}

public class GoEastEvent : GameEvent {
	public string someVar { get; private set; }

	public GoEastEvent(string someVar){
		this.someVar = someVar;
	}
}

public class GoSouthEvent : GameEvent {
	public string someVar { get; private set; }

	public GoSouthEvent(string someVar){
		this.someVar = someVar;
	}
}

public class GameOverEvent : GameEvent {
	public string someVar { get; private set; }

	public GameOverEvent(string someVar){
		this.someVar = someVar;
	}
}