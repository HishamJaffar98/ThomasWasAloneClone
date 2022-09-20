using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    [Header("Scrolling background Parameters")]
    [SerializeField] RawImage backgroundImage;
    [SerializeField] float xSpeed, ySpeed;


    [Header("Fader Parameters")]
    [SerializeField] GameObject fader;
    [SerializeField] Animator faderAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScrollBackgroundInfinitely();
        TransitionToScene();
    }

    private void TransitionToScene()
    {
        if (faderAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut") && faderAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            SceneManager.LoadScene(1);
        }
    }
    private void ScrollBackgroundInfinitely()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(xSpeed, ySpeed) * Time.deltaTime, backgroundImage.uvRect.size);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartButtonClicked()
	{
        fader.SetActive(true);
    }
}
