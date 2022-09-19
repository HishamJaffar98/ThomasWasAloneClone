using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimation))]
public abstract class CharacterControllerBase : MonoBehaviour
{
    #region Cached Components
    [SerializeField] protected CharacterMovement characterMovement;
    [SerializeField] protected CharacterAnimation characterAnimation;
    #endregion

    #region Variables
    [SerializeField] protected int movementSmoothingFactor = 2;
    protected float xRawInput;
    protected float smoothingValue;
    protected int xDirectionFlag=1;
    protected bool isActive = false;
    protected Vector2 spawnPoint;
    #endregion

    #region Events
    public static event Action<CharacterControllerBase> OnDeath;
    #endregion

    #region Unity Methods
    protected virtual void OnEnable()
	{
        CharacterCollisionHandler.OnHazardHit+= CloneAndDie;
        CheckPointManager.OnLatestCheckPointChanged += SetSpawnPoint;
    }

	protected virtual void Start()
    {
       
    }

    protected virtual void FixedUpdate()
	{
        SmoothOutXRawInput();
        UpdateMovement(smoothingValue);
    }

    protected virtual void Update()
	{
       
    }

    protected virtual void OnDisable()
    {
        CharacterCollisionHandler.OnHazardHit -= CloneAndDie;
        CheckPointManager.OnLatestCheckPointChanged -= SetSpawnPoint;
    }
    #endregion

    #region Private Methods
    private void SmoothOutXRawInput()
	{
        smoothingValue = Mathf.Lerp(smoothingValue, xRawInput, Time.deltaTime*movementSmoothingFactor);
	}

    private void UpdateMovement(float smoothingValue)
	{
        characterMovement.MovePlayer(smoothingValue);
	}

    private void CheckIfSpriteToBeFlipped(InputAction.CallbackContext value)
    {
        if (value.performed && xDirectionFlag != xRawInput && Mathf.Abs(xRawInput)==1)
        {
            xDirectionFlag *= -1;
            characterAnimation.FlipSprite(xDirectionFlag);
        }
    }

    private void SetSpawnPoint(Transform spawnPointTransform)
	{
        spawnPoint = spawnPointTransform.position;
	}

    private GameObject CloneInstance()
    {
        return GameObject.Instantiate(this.gameObject, spawnPoint, Quaternion.identity);
    }
	#endregion

	
	#region Public Methods
	public void OnMovement(InputAction.CallbackContext value)
	{
		Vector2 rawInput = value.ReadValue<Vector2>();
		xRawInput = rawInput.x;
		CheckIfSpriteToBeFlipped(value);
	}

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed && characterMovement.IsOnGround == true)
        {
            characterMovement.MakePlayerJump();
            characterAnimation.JumpSqueeze();
        }
    }

    public virtual void CloneAndDie(GameObject gameObjectToDestroy)
    {
        if(gameObjectToDestroy==this.gameObject)
		{
            gameObject.GetComponent<PlayerInput>().enabled = false;
            GameObject clone = CloneInstance();
            clone.GetComponent<PlayerInput>().enabled = true;
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
	#endregion

	#region Abstract Methods
	public abstract void Ability();
	#endregion
}
