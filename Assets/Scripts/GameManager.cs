using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Available Player Input Controllers")]
    [SerializeField] private PlayerInput[] playerInputControllers;

    #region Single Instance
    public static GameManager Instance;
	#endregion

	#region Variables
	int currentlyActiveInputIndex = 0;
	int playersDone = 0;
	const int MAX_PLAYER_COUNT = 3;
	#endregion

	#region Events
	public event Action<PlayerInput[],int> OnCharacterSelected;
	public event Action OnGameOver;
	#endregion

	#region Unity Methods
	private void Awake()
	{
		if(Instance!=null)
		{
            Destroy(gameObject);
		}
        else
		{
            Instance = this;
		}
	}

	private void OnEnable()
	{
		CharacterControllerBase.OnDeath += RefillInputControllerArray;
		ExitPoint.OnPlayerEnterExitPoint += IncrementPlayersDone;
		ExitPoint.OnPlayerLeaveExitPoint += DecrementPlayersDone;
	}

	void Start()
    {
        playerInputControllers[currentlyActiveInputIndex].enabled = true;
    }

	private void OnDisable()
	{
		CharacterControllerBase.OnDeath -= RefillInputControllerArray;
		ExitPoint.OnPlayerEnterExitPoint -= IncrementPlayersDone;
		ExitPoint.OnPlayerLeaveExitPoint -= DecrementPlayersDone;
	}
	#endregion

	#region Private Methods
	private void SetPlayableCharacter(int activePlayerIndex)
	{
		for (int i = 0; i < playerInputControllers.Length; i++)
		{
			if (i != activePlayerIndex)
			{
				playerInputControllers[i].enabled = false;
			}
		}
		playerInputControllers[activePlayerIndex].enabled = true;
		OnCharacterSelected?.Invoke(playerInputControllers, activePlayerIndex);
	}

	private void RefillInputControllerArray(CharacterControllerBase controllerBaseClass)
	{
		if(controllerBaseClass.GetType() == typeof(WaterCharacter))
		{
			playerInputControllers[0] = FindObjectOfType<WaterCharacter>().GetComponent<PlayerInput>();
		}
		if(controllerBaseClass.GetType() == typeof(FireCharacter))
		{
			playerInputControllers[1] = FindObjectOfType<FireCharacter>().GetComponent<PlayerInput>();
		}
		if (controllerBaseClass.GetType() == typeof(IceCharacter))
		{
			playerInputControllers[2] = FindObjectOfType<IceCharacter>().GetComponent<PlayerInput>();
		}
	}

	private void IncrementPlayersDone()
	{
		playersDone++;
		if(playersDone>=MAX_PLAYER_COUNT)
		{
			DisablePlayerInput();
			OnGameOver?.Invoke();
		}
	}

	private void DecrementPlayersDone()
	{
		playersDone--;
	}

	private void DisablePlayerInput()
	{
		foreach(PlayerInput playerInput in playerInputControllers)
		{
			playerInput.DeactivateInput();
		}
	}

	
	#endregion

	#region Public Methods
	public void OnSwitchToNext(InputAction.CallbackContext value)
    {
        if(value.performed)
		{
            if (currentlyActiveInputIndex == playerInputControllers.Length - 1)
            {
                currentlyActiveInputIndex = 0;
            }
            else
			{
                currentlyActiveInputIndex++;
            }
            SetPlayableCharacter(currentlyActiveInputIndex);
        }
    }

    public void OnSwitchToPrevious(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (currentlyActiveInputIndex == 0)
            {
                currentlyActiveInputIndex = playerInputControllers.Length - 1;
            }
            else
            {
                currentlyActiveInputIndex--;
            }
            SetPlayableCharacter(currentlyActiveInputIndex);
        }
    }
	#endregion
}
