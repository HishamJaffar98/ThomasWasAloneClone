using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SpriteFlipState {FlipRight, FlipLeft, NoFlip };
public enum JumpSqueezeStates { NormalToSqueeze, SqueezeToNormal, NoSqueeze };
public class CharacterAnimation : MonoBehaviour
{
	[Header("Character Sprite Reference")]
	[SerializeField] GameObject characterSprite;

	[Header("Flip Sprite Animation Parameters")]
	[SerializeField] private int spinAnimationSpeed;
	SpriteFlipState currentSpriteDirection = SpriteFlipState.NoFlip;
	float flipTimer = 0f;

	[Header("Jump Squeeze Animation Parameters")]
	[SerializeField] private Vector3 originalScale;
	[SerializeField] private Vector3 squeezeScale;
	[SerializeField] float squeezeInterval = 0.5f;
	private float squeezeTimer = 0f;
	JumpSqueezeStates currentJumpSqueezeState = JumpSqueezeStates.NoSqueeze;

	[Header("Selected Visual Reference")]
	[SerializeField] GameObject selectedVisual;

	private void OnEnable()
	{
		GameManager.Instance.OnCharacterSelected += ToggleSelectedVisual;
	}
	private void Start()
	{
		
	}

	private void Update()
	{
		FlipLeft();
		FlipRight();
		ChangeToSqueezeScale();
		ChangeToNormalScale();
	}

	private void OnDisable()
	{
		GameManager.Instance.OnCharacterSelected -= ToggleSelectedVisual;
	}

	private void FlipLeft()
	{
		if (currentSpriteDirection == SpriteFlipState.FlipLeft)
		{
			float newRotationAngle = Mathf.Lerp(0, 180, flipTimer);
			characterSprite.transform.rotation = Quaternion.AngleAxis(newRotationAngle, Vector2.up);
			flipTimer += Time.deltaTime * spinAnimationSpeed;
			if (flipTimer >= 1f)
			{
				currentSpriteDirection = SpriteFlipState.NoFlip;
			}
		}
	}

	private void FlipRight()
	{
		if (currentSpriteDirection == SpriteFlipState.FlipRight)
		{
			float newRotationAngle = Mathf.Lerp(180, 0, flipTimer);
			characterSprite.transform.rotation = Quaternion.AngleAxis(newRotationAngle, Vector2.up);
			flipTimer += Time.deltaTime * spinAnimationSpeed;
			if (flipTimer >= 1f)
			{
				currentSpriteDirection = SpriteFlipState.NoFlip;
			}
		}
	}

	private void ChangeToSqueezeScale()
	{
		if (currentJumpSqueezeState == JumpSqueezeStates.NormalToSqueeze)
		{
			squeezeTimer += Time.deltaTime;
			float interpolationValue = squeezeTimer / squeezeInterval;
			characterSprite.transform.localScale = Vector3.Lerp(originalScale, squeezeScale, interpolationValue);
			if (interpolationValue >= 1f)
			{
				squeezeTimer = 0f;
				currentJumpSqueezeState = JumpSqueezeStates.SqueezeToNormal;
			}
		}
	}

	private void ChangeToNormalScale()
	{
		if (currentJumpSqueezeState == JumpSqueezeStates.SqueezeToNormal)
		{
			squeezeTimer += Time.deltaTime;
			float interpolationValue = squeezeTimer / squeezeInterval;
			characterSprite.transform.localScale = Vector3.Lerp(squeezeScale, originalScale, interpolationValue);
			if (interpolationValue >= 1f)
			{
				squeezeTimer = 0f;
				currentJumpSqueezeState = JumpSqueezeStates.NoSqueeze;
			}
		}
	}

	private void ToggleSelectedVisual(PlayerInput[] playerInputArray, int activePlayerInputIndex)
	{
		if(gameObject.GetComponent<PlayerInput>()== playerInputArray[activePlayerInputIndex])
		{
			selectedVisual.SetActive(true);
		}
		else
		{
			selectedVisual.SetActive(false);
		}
	}

	public void FlipSprite(float directionFlag)
	{
		flipTimer = 0f;
		if (directionFlag==1)
		{
			currentSpriteDirection = SpriteFlipState.FlipRight;
		}
		else if(directionFlag==-1)
		{
			currentSpriteDirection = SpriteFlipState.FlipLeft;
		}
	}

	public void JumpSqueeze()
	{
		squeezeTimer = 0f;
		currentJumpSqueezeState = JumpSqueezeStates.NormalToSqueeze;
	}
}