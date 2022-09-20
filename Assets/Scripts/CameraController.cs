using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera[] characterCinemachines;
	[SerializeField] private float cameraShakeAmplitude=2f;
	[SerializeField] private float cameraShakeDuration=1f;
	private float cameraShakeTimer;
	private bool cameraShakeActive = false;
	private int activeCinemachineIndex = 0;

	private void OnEnable()
	{
		GameManager.Instance.OnCharacterSelected += SetCinemachinePriorities;
		CharacterControllerBase.OnDeath += CheckWhichCinmachineNeedsReset;
	}

	private void Update()
	{
		if (cameraShakeActive)
		{
			cameraShakeTimer += Time.deltaTime;
			float shakeAmplitudeInterpolationValue = cameraShakeTimer / cameraShakeDuration;
			characterCinemachines[activeCinemachineIndex].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain
				= Mathf.Lerp(cameraShakeAmplitude,0f, shakeAmplitudeInterpolationValue);
			if(shakeAmplitudeInterpolationValue >= 1f)
			{
				cameraShakeActive = false;
				cameraShakeTimer = 0f;
			}
		}
	}

	private void OnDisable()
	{
		GameManager.Instance.OnCharacterSelected -= SetCinemachinePriorities;
		CharacterControllerBase.OnDeath -= CheckWhichCinmachineNeedsReset;
	}

	private void SetCinemachinePriorities(PlayerInput[] dummyArray, int selectedCharacterIndex)
	{
		for(int i=0;i<characterCinemachines.Length;i++)
		{
			if(i==selectedCharacterIndex)
			{
				characterCinemachines[i].Priority = 1;
				activeCinemachineIndex = i;
			}
			else
			{
				characterCinemachines[i].Priority = 0;
			}
		}
	}

	private void CheckWhichCinmachineNeedsReset(CharacterControllerBase deadPlayerCharacterClass)
	{
		if(deadPlayerCharacterClass.GetType()==typeof(WaterCharacter))
		{
			GameObject newlySpawnedCharacter = FindObjectOfType<WaterCharacter>().gameObject;
			ResetCinemachineFollowAndTarget(newlySpawnedCharacter);
		}
		else if(deadPlayerCharacterClass.GetType() == typeof(FireCharacter))
		{
			GameObject newlySpawnedCharacter = FindObjectOfType<FireCharacter>().gameObject;
			ResetCinemachineFollowAndTarget(newlySpawnedCharacter);
		}
		else if(deadPlayerCharacterClass.GetType() == typeof(IceCharacter))
		{
			GameObject newlySpawnedCharacter = FindObjectOfType<IceCharacter>().gameObject;
			ResetCinemachineFollowAndTarget(newlySpawnedCharacter);
		}
	}

	private void ResetCinemachineFollowAndTarget(GameObject newlySpawnedCharacter)
	{
		characterCinemachines[activeCinemachineIndex].Follow = newlySpawnedCharacter.gameObject.transform;
		characterCinemachines[activeCinemachineIndex].LookAt = newlySpawnedCharacter.gameObject.transform;
		ActivatePlayerDeathCameraShake();
	}

	private void ActivatePlayerDeathCameraShake()
	{
		cameraShakeTimer = 0f;
		cameraShakeActive = true;
	}
}
