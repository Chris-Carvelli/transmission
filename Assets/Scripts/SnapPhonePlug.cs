using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPhonePlug : SnapPlug {
    public AudioSource speaker;
    public AudioClip whiteNoise;

    public override void PlugginAction () {
        base.PlugginAction();

        if (InSocket.CurrentMissionFake == null) {
            speaker.clip = whiteNoise;
            speaker.loop = true;
        }
        else {
            if (!InSocket.CurrentMissionFake.IsInConversation) {
                speaker.clip = InSocket.CurrentMissionFake.conversation.HelloOperator;
                speaker.loop = false;
            }

            else {
                InSocket.CurrentMissionFake.Play();
                speaker.time = InSocket.CurrentMissionFake.listeningtimer;
                speaker.clip = InSocket.CurrentMissionFake.conversation.Talk;
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
