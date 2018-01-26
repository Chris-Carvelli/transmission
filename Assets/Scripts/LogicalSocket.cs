using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalSocket : MonoBehaviour {
	public string Name
	{
		get
		{
			return this.gameObject.name;
		}
	}

	public bool IsLightOn
	{
		// TODO: figure out how to we should handle lights.
		// texture, point light, other?
		get;
		set;
	}
}
