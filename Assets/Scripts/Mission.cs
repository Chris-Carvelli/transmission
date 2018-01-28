using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
	public float timer = 0;
	public Conversation conversation;

	// not started (has been connected = false) (active = true)
	// timer started (has been connected = true) (active = true) (sound playing = false)
	// sound started (has been connected = true) (active = true) (sound playing = true)
	// detached (has been connected=true) (active = false)

	public bool IsInConversation
	{
		get
		{
			return has_been_connected && active;
		}
	}

	bool has_been_connected = false;
	public bool active = true;

	bool isplaying
	{
		get
		{
			if(conversation == null || conversation.Talk == null)
			{
				return false;
			}
			return listening && listeningtimer < conversationlength;
		}
	}

	float conversationlength
	{
		get
		{
			return conversation.Talk.length;
		}
	}

	private bool listening = false;
	public float listeningtimer = 0;
	public void Play()
	{
		listening = true;
	}

	float timer_length
	{
		get
		{
			// TODO: get actual length of sound?
			return 30.0f;
		}
	}

	public void Step(float dt)
	{
		if(active == false)
		{
			return;
		}
		var c = this.conversation;
		if(c == null ){return;}
		if(c.FromCaller == null) return;
		if(c.ToCaller == null) return;

		c.FromCaller.CurrentMission = this;

		var fp = c.FromCaller.ConnectedPlug;
		var tp = c.ToCaller.ConnectedPlug;
		var connected = fp != null && tp != null && fp.OtherSide == tp && tp.OtherSide == fp;
		if(connected && !has_been_connected)
		{
			Debug.Log("Mission connected, timer started");
			has_been_connected = true;

			c.ToCaller.CurrentMission = this;
		}

		if(this.IsInConversation && !c.HasHeard && isplaying)
		{
			Debug.Log("Has heard a conversation, wont be played again");
			c.HasHeard = true;
		}

		if(this.IsInConversation && c.HasHeard && !isplaying)
		{
			Debug.Log("Sound has stopped");
			active = false;
		}

		if(this.IsInConversation && !c.HasHeard)
		{
			timer += dt;
			if(timer >= timer_length)
			{
				Debug.Log("Timer has expired. Killing conversation.");
				active = false;
			}
		}
		
		if(listening)
		{
			listeningtimer += dt;
		}

		if(active && !connected && has_been_connected)
		{
			Debug.Log("Cable yanked.");
			active = false;
		}
		
		if(!active)
		{
			// TODO: remove self from callers to make them stop blinking
			if(c.FromCaller.CurrentMission == this)
			{
				c.FromCaller.CurrentMission = null;
			}
			if(c.ToCaller.CurrentMission == this)
			{
				c.ToCaller.CurrentMission = null;
			}
		}
	}
}
