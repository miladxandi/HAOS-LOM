using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace HAOS.General
{
    public class CameraControls : MonoBehaviour
    {
        public Button Load;
        private void Start()
        {
            Load.onClick.AddListener(StartTour);
        }
        public void StartTour()
        {
            PlayerPrefs.DeleteKey("SceneLoad");
            PlayerPrefs.SetString("SceneLoad", "Mars1");
            SceneManager.LoadScene("Loading");
        }
    }
}
