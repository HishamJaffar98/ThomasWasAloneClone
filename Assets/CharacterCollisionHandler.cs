using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterCollisionHandler : MonoBehaviour
{
    #region Events
    public static event Action<GameObject> OnCheckPointHit;
	public static event Action<GameObject> OnHazardHit;
	#endregion

	#region Private Methods
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == TagManager.CheckPoint)
		{
			OnCheckPointHit?.Invoke(collision.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == TagManager.Hazard)
		{
			OnHazardHit?.Invoke(gameObject);
		}
	}
	#endregion
}
