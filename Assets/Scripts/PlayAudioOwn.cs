using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOwn : MonoBehaviour {

    public enum EventType
    {
        Start,
        Awake,
        OnDestroy,
        OnCollisionEnter,
        OnCollisionExit,
        OnTriggerEnter,
        OnTriggerExit,
        OnEnable,
        OnDisable
    }

    protected void Start()
    {
        _CheckEvent(EventType.Start);
    }

    protected void OnDestroy()
    {
        if (triggerEvent == EventType.OnDestroy && AudioController.DoesInstanceExist()) // otherwise errors can occur if the entire scene gets destroyed
        {
            _CheckEvent(EventType.OnDestroy);
        }
    }

    protected void OnCollisionEnter()
    {
        _CheckEvent(EventType.OnCollisionEnter);
    }

    protected void OnCollisionExit()
    {
        _CheckEvent(EventType.OnCollisionExit);
    }

    protected void OnTriggerEnter()
    {
        _CheckEvent(EventType.OnTriggerEnter);
    }

    protected void OnTriggerExit()
    {
        _CheckEvent(EventType.OnTriggerExit);
    }

    protected void OnEnable()
    {
        _CheckEvent(EventType.OnEnable);
    }

    protected void OnDisable()
    {
        _CheckEvent(EventType.OnDisable);
    }

    protected void _CheckEvent(EventType eventType)
    {
        if (triggerEvent == eventType)
        {
            _OnEventTriggered();
        }
    }

    public enum PlayPosition
    {
        Global,
        ChildObject,
        ObjectPosition,
    }

    public enum SoundType
    {
        SFX = 0,
        Music = 1,
        AmbienceSound = 2,
    }

    public string audioID;
    public SoundType soundType = SoundType.SFX;
    public PlayPosition position = PlayPosition.Global; // has no meaning for Music
    public EventType triggerEvent = EventType.Start;
    public float volume = 1;
    public float delay = 0;
    public float startTime = 0;

    protected void Awake()
    {
        if (triggerEvent == EventType.OnDestroy && position == PlayPosition.ChildObject)
        {
            position = PlayPosition.ObjectPosition;
            Debug.LogWarning("OnDestroy event can not be used with ChildObject");
        }
        _CheckEvent(EventType.Awake);
    }

    private void _Play()
    {
        switch (position)
        {
            case PlayPosition.Global:
                AudioController.Play(audioID, volume, delay, startTime); break;
            case PlayPosition.ChildObject:
                AudioController.Play(audioID, transform, volume, delay, startTime); break;
            case PlayPosition.ObjectPosition:
                AudioController.Play(audioID, transform.position, null, volume, delay, startTime); break;
        }
    }

    protected void _OnEventTriggered()
    {
        if (string.IsNullOrEmpty(audioID)) return;

        switch (soundType)
        {
            case SoundType.SFX: _Play(); break;
            case SoundType.Music: _PlayMusic(); break;
            case SoundType.AmbienceSound: _PlayAmbienceSound(); break;
        }
    }

    private void _PlayMusic()
    {
        switch (position)
        {
            case PlayPosition.Global:
                AudioController.PlayMusic(audioID, volume, delay, startTime); break;
            case PlayPosition.ChildObject:
                AudioController.PlayMusic(audioID, transform, volume, delay, startTime); break;
            case PlayPosition.ObjectPosition:
                AudioController.PlayMusic(audioID, transform.position, null, volume, delay, startTime); break;
        }
    }

    private void _PlayAmbienceSound()
    {
        switch (position)
        {
            case PlayPosition.Global:
                AudioController.PlayAmbienceSound(audioID, volume, delay, startTime); break;
            case PlayPosition.ChildObject:
                AudioController.PlayAmbienceSound(audioID, transform, volume, delay, startTime); break;
            case PlayPosition.ObjectPosition:
                AudioController.PlayAmbienceSound(audioID, transform.position, null, volume, delay, startTime); break;
        }
    }
}
