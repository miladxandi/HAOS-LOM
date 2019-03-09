using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public Button YES, NO;

    public Canvas ExitCanvas;

    public Animator ExitAnimation;

    private void Start()
    {
        
        YES.onClick.AddListener(YesMethod);
        NO.onClick.AddListener(NoMethod);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitAnimation.SetBool("Animate",true);
            ExitCanvas.gameObject.SetActive(true);
        }
    }
    public void YesMethod()
    {
        Application.Quit();
    }
    public void NoMethod()
    {
        ExitAnimation.SetBool("Animate",false);
        ExitCanvas.gameObject.SetActive(false);
    }
}
