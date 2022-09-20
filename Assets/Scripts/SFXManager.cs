using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip checkpointSFX;
    [SerializeField] private AudioClip switchHitSFX;
    [SerializeField] private AudioClip mouseHoverUISFX;
    [SerializeField] private AudioClip mouseClickUISFX;

    public static SFXManager instance;
    private AudioSource sfxManagerAS;

	private void OnEnable()
	{
        CharacterControllerBase.OnDeath += PlayDeathSFX;
        CharacterCollisionHandler.OnCheckPointHit += PlayCheckPointSFX;
        Switch.OnSwitchHit += PlaySwitchHitSFX;
        CharacterMovement.OnCharacterJumped += PlayJumpSFX;
    }

	private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        sfxManagerAS = GetComponent<AudioSource>();
    }

	private void OnDisable()
	{
        CharacterControllerBase.OnDeath -= PlayDeathSFX;
        CharacterCollisionHandler.OnCheckPointHit -= PlayCheckPointSFX;
        Switch.OnSwitchHit -= PlaySwitchHitSFX;
        CharacterMovement.OnCharacterJumped -= PlayJumpSFX;
    }

    public void PlayMouseHoverSFX()
	{
        sfxManagerAS.PlayOneShot(mouseHoverUISFX);
    }

    public void PlayMouseClickSFX()
    {
        sfxManagerAS.PlayOneShot(mouseClickUISFX);
    }

    public void PlayDeathSFX(CharacterControllerBase value)
    {
        sfxManagerAS.PlayOneShot(deathSFX);
    }

    public void PlayCheckPointSFX(GameObject value)
    {
        sfxManagerAS.PlayOneShot(checkpointSFX);
    }

    public void PlaySwitchHitSFX()
    {
        sfxManagerAS.PlayOneShot(switchHitSFX);
    }

    private void PlayJumpSFX()
    {
        sfxManagerAS.PlayOneShot(jumpSFX);
    }
}
