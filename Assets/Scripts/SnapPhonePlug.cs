using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPhonePlug : SnapPlug {
    public AudioSource speaker;
    public AudioClip whiteNoise;

    public override void PlugginAction () {
        base.PlugginAction();

        if (InSocket.CurrentMission == null) {
            speaker.clip = whiteNoise;
            speaker.loop = true;
        }
        else {
            if (!InSocket.CurrentMission.IsInConversation) {
                speaker.clip = InSocket.CurrentMission.conversation.HelloOperator;
                speaker.loop = false;
            }

            else {
                InSocket.CurrentMission.Play();
                speaker.time = InSocket.CurrentMission.listeningtimer;
                speaker.clip = InSocket.CurrentMission.conversation.Talk;
                speaker.loop = false;
                speaker.Play();
            }
        }
    }

    public override void UnplugginAction() {
        base.UnplugginAction();

        speaker.clip = whiteNoise;
        speaker.loop = true;

    }
}
