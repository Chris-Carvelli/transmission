using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour {

	public AudioSource From;
	public AudioSource To;
	public AudioSource Talk;

	void Start ()
	{
		FromTo(this.From);
		FromTo(this.To);
		if(this.Talk != null)
		{
			this.Talk.loop = false;
			this.Talk.playOnAwake = false;
		}
	}

	private static void FromTo(AudioSource f)
	{
		if(f == null) { return; }
		f.playOnAwake = false;
		f.loop = false;
	}
	
	void Update ()
	{
	}

	public bool IsInConversation
	{
		get
		{
			if(this.Talk == null) return false;
			return this.Talk.isPlaying;
		}
	}
}
