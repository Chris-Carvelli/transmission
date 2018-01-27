using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {
	// set this in unity editor
	public SnapSocket[] Sockets;

	public Conversation[] Conversations;

	// mission holds a connection request
	private class Mission
	{
		public SnapSocket From;
		public SnapSocket To;
		public Conversation conversation;
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

	Conversation FindConversation()
	{
		List<Conversation> conv = new List<Conversation>();
		foreach(var c in this.Conversations)
		{
			conv.Add(c);
		}
		foreach(var m in this.missions)
		{
			conv.Remove(m.conversation);
		}

		return SelectRandomOrNull(conv);
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

		return SelectRandomOrNull(free_sockets);
	}

	private static T SelectRandomOrNull<T>(List<T> free_sockets)
	{
		if(free_sockets.Count == 0) 
		{
			return default(T);
		}
		var index = Random.Range(0, free_sockets.Count);
		return free_sockets[index];
	}

	// create a mission if possible
	public void CreateNewMission()
	{
		var conv = FindConversation();
		if(conv == null)
		{
			return;
		}
		var from = FindRandomFreeSocket(null);
		if(from == null)
		{
			return;
		}
		var to = FindRandomFreeSocket(from);
		if(to == null)
		{
			return;
		}
		var m = new Mission();
		m.From = from;
		m.To = to;
		m.conversation = conv;
		// TODO: start socket blinking and stuff
		this.missions.Add(m);
	}
	
}
