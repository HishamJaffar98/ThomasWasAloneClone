using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Cached Components
    [SerializeField] private Rigidbody2D rb2D;
    #endregion

    #region Structs
    [SerializeField] LayerMask groundLayerMask;
	#endregion

	#region Variables
	[SerializeField] private int movementSpeed = 20;
    [SerializeField] private float jumpPower = 20;
    [SerializeField] private float fallSpeed = 5; 
    [SerializeField] private int fallMultiplier = 5;
    private float originalGravityScale;
    private bool isOnGround;
	#endregion

	#region Unity Methods
	void Start()
    {
        originalGravityScale = rb2D.gravityScale;
    }

    private void FixedUpdate()
    {
        isOnGround = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayerMask);
    }
	void Update()
	{
		AlterGravityScale();
	}
	#endregion

	#region Private Methods
	private void AlterGravityScale()
	{
		if (rb2D.velocity.y < fallSpeed && !isOnGround)
		{
			rb2D.gravityScale = fallMultiplier;
		}
		if (isOnGround)
		{
			rb2D.gravityScale = originalGravityScale;
		}
	}
	#endregion

	#region Public Methods
	public void MovePlayer(float smoothedOutDirection)
    {
        Vector2 newPosition = new Vector2(transform.position.x + (smoothedOutDirection * movementSpeed * Time.deltaTime), transform.position.y);
        transform.position = newPosition;
    }

	public void MakePlayerJump()
	{
        if(isOnGround)
		{
            rb2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse ); 
        }
	}
	#endregion
}
