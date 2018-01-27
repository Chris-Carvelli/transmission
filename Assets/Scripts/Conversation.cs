using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour {
	public AudioSource HelloOperator;
	public SnapSocket FromCaller;
	public SnapSocket ToCaller;
	// public float seconds;
	public AudioSource Talk;

	public bool HasHeard = false;

	void Start ()
	{
		FromTo(this.HelloOperator);
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
}
