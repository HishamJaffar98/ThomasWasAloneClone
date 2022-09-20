using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExitPoint : MonoBehaviour
{
	public static event Action OnPlayerEnterExitPoint;
	public static event Action OnPlayerLeaveExitPoint;

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag== gameObject.tag)
		{
			OnPlayerEnterExitPoint?.Invoke();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == gameObject.tag)
		{
			OnPlayerLeaveExitPoint?.Invoke();
		}
	}
}
