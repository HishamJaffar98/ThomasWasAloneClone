using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckPointManager : MonoBehaviour
{
	public static event Action<Transform> OnLatestCheckPointChanged;
	[SerializeField] private GameObject lastCheckpointReached;
	private int latestCheckpointIndex = -1;
	private void OnEnable()
	{
		CharacterCollisionHandler.OnCheckPointHit += SetLatestCheckpointReached;
	}

	private void OnDisable()
	{
		CharacterCollisionHandler.OnCheckPointHit -= SetLatestCheckpointReached;
	}
	
	private void SetLatestCheckpointReached(GameObject checkpointHit)
	{
		if(checkpointHit.transform.GetSiblingIndex()>latestCheckpointIndex)
		{
			lastCheckpointReached = checkpointHit;
			latestCheckpointIndex = checkpointHit.transform.GetSiblingIndex();
			OnLatestCheckPointChanged?.Invoke(lastCheckpointReached.transform);
		}
	}
}
