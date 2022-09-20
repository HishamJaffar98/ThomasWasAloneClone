using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Switch : MonoBehaviour
{
    [SerializeField] private Animator connectedDoorAnimator;
    private Animator switchAnimator;

    public static event Action OnSwitchHit;
    void Start()
    {
        switchAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == gameObject.tag)
        {
            OnSwitchHit?.Invoke();
            switchAnimator.SetTrigger("switchTriggered");
        }
    }

    public void TriggerDoor()
	{
        connectedDoorAnimator.SetTrigger("doorTriggered");
    }
}
