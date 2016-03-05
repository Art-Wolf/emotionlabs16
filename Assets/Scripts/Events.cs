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
