using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {
	// set this in unity editor
	public Conversation[] Conversations;

	// mission holds a connection request
	private class Mission
	{
		public Conversation conversation;
	}

	List<Mission> missions = new List<Mission>();

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
		var m = new Mission();
		m.conversation = conv;
		// TODO: start socket blinking and stuff
		this.missions.Add(m);
	}
	
}
