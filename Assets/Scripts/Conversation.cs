using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour {
	public AudioClip HelloOperator;
	public SnapSocket FromCaller;
	public SnapSocket ToCaller;
	public AudioClip Talk;

	public bool HasHeard = false;

	void Start ()
	{
	}
	
	void Update ()
	{
	}
}
