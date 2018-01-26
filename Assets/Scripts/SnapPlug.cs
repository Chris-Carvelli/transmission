using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// plug is inserted into socket
public class SnapPlug : MonoBehaviour {

	// set in unity editor
	public SnapPlug OtherSide;

	// the socket this is connected to (only for reading)
	// todo add some event handling?
	public SnapSocket InSocket
	{
		get;
		private set;
	}

	// called from snap socket
	public void SetSocket(SnapSocket socket)
	{
		this.InSocket = socket;
	}
}
