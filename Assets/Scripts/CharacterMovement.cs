using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Cached Components
    [SerializeField] private Rigidbody2D rb2D;
    #endregion

    #region Variables
    [SerializeField] private int movementSpeed = 20;
    [SerializeField] private int jumpPower = 20;
    [SerializeField] private int fallMultiplier = 5;
    private float originalGravityScale=1f;
    #endregion

    void Start()
    {
        originalGravityScale = rb2D.gravityScale;
    }

    void Update()
    {
        if(rb2D.velocity.y<0)
		{
            rb2D.gravityScale += fallMultiplier;
        }
    }

    public void MovePlayer(float smoothedOutDirection)
    {
        Vector2 newPosition = new Vector2(transform.position.x + (smoothedOutDirection * movementSpeed * Time.deltaTime), transform.position.y);
        transform.position = newPosition;
    }

	public void MakePlayerJump()
	{
        rb2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
	}
}
