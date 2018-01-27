using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {
	// set this in unity editor
	public Conversation[] Conversations;

	List<Mission> missions = new List<Mission>();

	Conversation FindConversation()
	{
		List<Conversation> conv = new List<Conversation>();
		foreach(var c in this.Conversations)
		{
			if(!c.HasHeard && !IsInConversation(c.FromCaller))
			{
				conv.Add(c);
			}
		}

		// dont include the currently active missions
		foreach(var m in this.missions)
		{
			conv.Remove(m.conversation);
		}

		return SelectRandomOrNull(conv);
	}

	private bool IsInConversation(SnapSocket socket)
	{
		foreach(var m in missions)
		{
			// a person calling, cant call again
			if(m.conversation.FromCaller == socket)
			{
				return true;
			}

			if(m.conversation.ToCaller == socket &&
				m.IsInConversation)
			{
				return true;
			}
		}

		return false;
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
