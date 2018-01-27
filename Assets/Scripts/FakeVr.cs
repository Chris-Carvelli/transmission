using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeVr : MonoBehaviour {
	public SnapPlug CableStart;
	public SnapPlug CableEnd;

	public SnapSocket Left;
	public SnapSocket Right;

	public Gameplay gameplay;
	
	void Start ()
	{
	}
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Toggle(Left, CableStart);
		}

		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			Toggle(Right, CableEnd);
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			gameplay.CreateNewMission();
		}
	}

	void Toggle(SnapSocket socket, SnapPlug plug)
	{
		Debug.Log("Toggle");
		socket.ConnectedPlug = socket.ConnectedPlug == null ? plug : null;
	}
}
