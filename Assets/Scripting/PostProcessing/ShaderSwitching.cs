using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ShaderSwitching : MonoBehaviour {

    public List<PostProcessingProfile> myPostProcessingProfiles;

	private PostProcessingBehaviour myPostProcessingBehaviour;
	private int myCurrentIndex;

	// Use this for initialization
	void Start () {
		myPostProcessingBehaviour = GetComponent<PostProcessingBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		if (myPostProcessingProfiles != null && myPostProcessingProfiles.Count > 0) {
			bool keyPressed = false;
			if (Input.GetKeyDown (KeyCode.Q)) {
				myCurrentIndex--;
				if (myCurrentIndex < 0) {
					myCurrentIndex = myPostProcessingProfiles.Count - 1;
				}
				keyPressed = true;
			}
			else if (Input.GetKeyDown (KeyCode.E)) {
				myCurrentIndex++;
				if (myCurrentIndex > myPostProcessingProfiles.Count-1) {
					myCurrentIndex = 0;
				}
				keyPressed = true;
			}
			if (keyPressed) {
				DestroyImmediate(gameObject.GetComponent<PostProcessingBehaviour> ());
				PostProcessingBehaviour myPostProcessingBehaviour = gameObject.AddComponent<PostProcessingBehaviour>();
				myPostProcessingBehaviour.profile = myPostProcessingProfiles[myCurrentIndex];
				keyPressed = false;
				Debug.Log("set new!");
			}
		}
	}
}
