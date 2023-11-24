using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnableAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmClip;
    [SerializeField] private float _rampSpeed;

    private float _minVolume = 0;
    private float _maxVolume = 500;

    private Coroutine _activationCoroutine;
    private Coroutine _deactivationCoroutine;

    private void Start()
    {
        _alarmClip.volume = 0;
        _alarmClip.Play();
    }

    public void StartAlarm()
    {
        if (_deactivationCoroutine != null)
            StopCoroutine(_deactivationCoroutine);

        _activationCoroutine = StartCoroutine(Activate());
    }

    public void StopAlarm()
    {
        if (_activationCoroutine != null)
            StopCoroutine(_activationCoroutine);

        _deactivationCoroutine = StartCoroutine(Deactivate());
    }

    public IEnumerator Activate()
    {
        while (_alarmClip.volume < _maxVolume)
        {
            _alarmClip.volume = Mathf.MoveTowards(_alarmClip.volume, _maxVolume, _rampSpeed);

            yield return null;
        }
    }

    public IEnumerator Deactivate()
    {
        while (_alarmClip.volume > _minVolume)
        {
            _alarmClip.volume = Mathf.MoveTowards(_alarmClip.volume, _minVolume, _rampSpeed);

            yield return null;
        }
    }
}
