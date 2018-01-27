using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
	// how many seconds a light is on/off when blinking
	public float BlinkInterval = 0.5f;
	private float timer = 0;

	// objects to enable and disable
	public GameObject[] OnObjects;
	public GameObject[] OffObjects;

	public MeshRenderer[] Meshes;
	public Material[] OnMaterials;
	public Material[] OffMaterial;


	private bool blink = true;
	public bool IsBlinking
	{
		get{return this.blink;}
		set
		{
			this.blink = value;
			if(!value)
			{
				// not blinking, set to false
				this.IsOn = false;

				// reset timer for when starting to blink again
				this.ResetTimer();
			}
		}
	}

	private bool ison;
	public bool IsOn
	{
		get
		{
			return ison;
		}

		private set{
			ison = value;
			SetGos(value);
		}
	}

	private void SetGos(bool v)
	{
		// TODO: play some click sound when switching?
		// TODO: set active or set visiblity or something else?
		foreach(var g in this.OnObjects)
		{
			g.SetActive(v);
		}

		foreach(var g in this.OffObjects)
		{
			g.SetActive(!v);
		}

		for(int i=0; i<this.Meshes.Length; i+=1)
		{
			var m = this.Meshes[i];
			var o = v ? this.OnMaterials[i] : this.OffMaterial[i];
			m.material = o;
		}
	}

	void Start()
	{
		this.ResetTimer();
	}

	private void ResetTimer()
	{
		this.timer = Random.value * this.BlinkInterval * 0.7f;
	}

	void Update ()
	{
		if(this.IsBlinking)
		{
			timer += Time.deltaTime;
			if(timer >= this.BlinkInterval)
			{
				timer -= this.BlinkInterval;
				this.IsOn = !this.IsOn;
			}
		}
	}
}
