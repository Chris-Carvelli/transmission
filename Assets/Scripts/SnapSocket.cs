using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// socket requires plug
public class SnapSocket : MonoBehaviour {
	SnapPlug connected_plug_;

	// the conencted plug
	// TODO add some events?
	public SnapPlug ConnectedPlug
	{
		get
		{
			return connected_plug_;
		}

		set
		{
			if(value && connected_plug_ != null)
			{
				connected_plug_.SetSocket(null);
			}
			connected_plug_ = value;
			if(connected_plug_ != null)
			{
				connected_plug_.SetSocket(this);
			}
		}
	}
}
