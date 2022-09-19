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
	#endregion

	#region Events
	public event Action<PlayerInput> OnCharacterSelected;
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
	void Start()
    {
        playerInputControllers[currentlyActiveInputIndex].enabled = true;
    }
    void Update()
    {

    }
	#endregion

	#region Private Methods
	private void SetPlayableCharacter(int activePlayerIndex)
	{
        for(int i=0;i< playerInputControllers.Length;i++)
		{
            if(i!=activePlayerIndex)
			{
                playerInputControllers[i].enabled = false;
            }
        }
        playerInputControllers[activePlayerIndex].enabled = true;
        OnCharacterSelected?.Invoke(playerInputControllers[activePlayerIndex]);
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
