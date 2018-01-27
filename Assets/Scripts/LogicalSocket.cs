using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalSocket : MonoBehaviour
{
	public string Name
	{
		get
		{
			return this.gameObject.name;
		}
	}

	void Start()
	{
		this.Blinking = this.GetComponent<Blink>();
	}

	Blink Blinking;

	public bool HasCallWaiting
	{
		get { return this.Blinking.IsBlinking; }
	}
}
