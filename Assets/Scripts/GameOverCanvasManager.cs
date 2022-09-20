using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Buttons { None, Restart, Menu, Quit, Start };
public class GameOverCanvasManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvasBaseContainer;

    [Header("Star particle system Parameters")]
    [SerializeField] ParticleSystem starsParticleSystem;
    [SerializeField] Transform starsParticleOrigin;

    [Header("Fader Parameters")]
    [SerializeField] GameObject fader;
    [SerializeField] Animator faderAnimator;

    private Buttons buttonSelected = Buttons.None;

	private void OnEnable()
	{
        GameManager.Instance.OnGameOver += InstantiateGameOverCanvas;
	}

    private void Start()
    {
        TransitionToScene();
    }

    private void Update()
    {
        TransitionToScene();
    }

	private void OnDisable()
	{
        GameManager.Instance.OnGameOver -= InstantiateGameOverCanvas;
    }

    private void InstantiateGameOverCanvas()
	{
        gameOverCanvasBaseContainer.SetActive(true);
        Instantiate(starsParticleSystem, starsParticleOrigin);
    }

	private void TransitionToScene()
    {
        if (faderAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut") && faderAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            switch(buttonSelected)
			{
                case Buttons.Restart:
                    SceneManager.LoadScene(1);
                    break;
                case Buttons.Menu:
                    SceneManager.LoadScene(0);
                    break;
                case Buttons.Quit:
                    Application.Quit();
                    break;
            }
        }
    }

    public void RestartButtonClicked()
    {
        fader.SetActive(true);
        buttonSelected = Buttons.Restart;
    }
    
    public void MenuButtonClicked()
    {
        fader.SetActive(true);
        buttonSelected = Buttons.Menu;
    }

    public void QuitGameButtonClicked()
    {
        fader.SetActive(true);
        buttonSelected = Buttons.Quit;
    }

 
}
