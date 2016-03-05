using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEvent
{
}

public class GoWestEvent : GameEvent {
	public string emotion { get; private set; }

	public GoWestEvent(string emotion){
		this.emotion = emotion;
	}
}

public class GoNorthEvent : GameEvent {
	public string emotion { get; private set; }

	public GoNorthEvent(string someVar){
		this.emotion = emotion;
	}
}

public class GoEastEvent : GameEvent {
	public string emotion { get; private set; }

	public GoEastEvent(string emotion){
		this.emotion = emotion;
	}
}

public class GoSouthEvent : GameEvent {
	public string emotion { get; private set; }

	public GoSouthEvent(string emotion){
		this.emotion = emotion;
	}
}

public class GameOverEvent : GameEvent {
	public string someVar { get; private set; }

	public GameOverEvent(string someVar){
		this.someVar = someVar;
	}
}