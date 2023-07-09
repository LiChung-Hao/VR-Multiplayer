using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Voice.Unity;
using System;
public class VoiceDebugUI : MonoBehaviour
{
  
    [SerializeField]
    private TextMeshProUGUI voiceState;
 
    private PunVoiceClient punVoiceClient;
    
    private void Awake()
    {
        this.punVoiceClient = PunVoiceClient.Instance;    
    }

    private void OnEnable()
    {
       this.punVoiceClient.Client.StateChanged += this.VoiceClientStateChanged;     
    }

    private void OnDisable()
    {
        this.punVoiceClient.Client.StateChanged -= this.VoiceClientStateChanged;     
    }

    private void Update()
    {
        if (this.punVoiceClient == null)
        {
            this.punVoiceClient = PunVoiceClient.Instance;
        }       
    }

    
    private void VoiceClientStateChanged(Photon.Realtime.ClientState fromState, Photon.Realtime.ClientState toState)
    {
        this.UpdateUiBasedOnVoiceState(toState);
    }

    private void UpdateUiBasedOnVoiceState(Photon.Realtime.ClientState voiceClientState)
    {
        this.voiceState.text = string.Format("PhotonVoice: {0}", voiceClientState);
        if (voiceClientState == Photon.Realtime.ClientState.Joined)
        {
            voiceState.gameObject.SetActive(false);
        }       
    }
}

