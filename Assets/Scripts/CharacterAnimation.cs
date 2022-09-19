using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SpriteFlipState {FlipRight, FlipLeft, NoFlip };
public class CharacterAnimation : MonoBehaviour
{
	[SerializeField] private int spinAnimationSpeed;
	SpriteFlipState currentSpriteDirection = SpriteFlipState.NoFlip;
	float flipTimer = 0f;
	private void Start()
	{
		
	}

	private void Update()
	{
		FlipLeft();
		FlipRight();
	}

	private void FlipLeft()
	{
		if (currentSpriteDirection == SpriteFlipState.FlipLeft)
		{
			float newRotationAngle = Mathf.Lerp(0, 180, flipTimer);
			transform.rotation = Quaternion.AngleAxis(newRotationAngle, Vector2.up);
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
			transform.rotation = Quaternion.AngleAxis(newRotationAngle, Vector2.up);
			flipTimer += Time.deltaTime * spinAnimationSpeed;
			if (flipTimer >= 1f)
			{
				currentSpriteDirection = SpriteFlipState.NoFlip;
			}
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
}