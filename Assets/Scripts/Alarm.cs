using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private UnityEvent _entered;
    [SerializeField] private UnityEvent _exit;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Crook>(out Crook crook))
        {
            _entered.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.TryGetComponent<Crook>(out Crook crook))
        {
            _exit.Invoke();
        }
    }
}


