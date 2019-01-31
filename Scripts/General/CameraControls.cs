using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace HAOS.General
{
    public class CameraControls : MonoBehaviour
    {
        public Button StartButton;
        // Use this for initialization
        void Start()
        {
            StartButton.onClick.AddListener(StartTour);
        }
        public void StartTour()
        {
            SceneManager.LoadScene("Mars1");
        }
    }
}
