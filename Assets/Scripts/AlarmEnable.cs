using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmEnable : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmClip;
    [SerializeField] private float _rampSpeed;

    private float _minVolume = 0;
    private float _maxVolume = 500;

    private Coroutine _volumeChangeCoroutine;
    private bool _increasingVolume = false;

    public void Increase()
    {
        Enable(_alarmClip.volume, _maxVolume, _rampSpeed);
    }

    public void Decrease()
    {
        Enable(_alarmClip.volume, _minVolume, _rampSpeed);
    }

    private void Start()
    {
        _alarmClip.volume = 0;
        _alarmClip.Play();
    }

    private void Enable(float currentVolume, float neededVolume, float speed)
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }

        _increasingVolume = neededVolume > currentVolume;
        _volumeChangeCoroutine = StartCoroutine(ChangeVolume(currentVolume, neededVolume, speed));
    }

    private IEnumerator ChangeVolume(float currentVolume, float neededVolume, float speed)
    {
        while ((_increasingVolume && currentVolume < neededVolume) || (!_increasingVolume && currentVolume > neededVolume))
        {
            currentVolume = Mathf.MoveTowards(currentVolume, neededVolume, speed);
            _alarmClip.volume = currentVolume;

            yield return null;
        }
    }
}