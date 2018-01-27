using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {
	// set this in unity editor
	public SnapSocket[] Sockets;

	// mission holds a connection request
	private class Mission
	{
		public SnapSocket From;
		public SnapSocket To;
	}

	List<Mission> missions = new List<Mission>();

	// tests if a socket is ready from connection
	bool IsFree(SnapSocket socket)
	{
		if(socket.ConnectedPlug != null) 
		{
			// this socket is already connected to a plug, so its not free
			return false;
		}

		foreach(var m in this.missions)
		{
			if(socket == m.From || socket == m.To)
			{
				// this socket is already assigned to a mission, so its not free
				return false;
			}
		}

		return true;
	}

	// returns null if no free socket can be found
	private SnapSocket FindRandomFreeSocket(SnapSocket not_this_socket)
	{
		// collect all free sockets
		List<SnapSocket> free_sockets = new List<SnapSocket>();
		foreach(var s in this.Sockets)
		{
			if(s != not_this_socket && IsFree(s))
			{
				free_sockets.Add(s);
			}
		}

		if(free_sockets.Count == 0)
		{
			return null;
		}

		var index = Random.Range(0, free_sockets.Count);
		return free_sockets[index];
	}

	// create a mission if possible
	public void CreateNewMission()
	{
		var from = FindRandomFreeSocket(null);
		if(from == null)
		{
			return;
		}
		var to = FindRandomFreeSocket(from);
		if(tag == null)
		{
			return;
		}
		var m = new Mission();
		m.From = from;
		m.To = to;
		// TODO: start socket blinking and stuff
		this.missions.Add(m);
	}
	
}
