using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace HAOS.Scene.LOM
{
    public class Entrance : MonoBehaviour
    {
        public GameObject MainCamera;
        public Image Menu;
        public Text PlayerName,StartGame;
        public Animator MenuAnimator;

        // Use this for initialization
        void Start()
        {
            MenuAnimator = MainCamera.GetComponent<Animator>();
            PlayerName.text = string.IsNullOrEmpty(PlayerPrefs.GetString("Name"))!=true ? PlayerPrefs.GetString("Name") : "ENTER YOUR NAME";
            if (PlayerPrefs.GetInt("SceneManager") == 0 || PlayerPrefs.GetInt("SceneManager") == null)
            {
                PlayerPrefs.SetInt("SceneManager", 0);
            }
            Menu.GetComponent<Button>().onClick.AddListener(MenuAnime);
            PlayerName.GetComponent<Button>().onClick.AddListener(SetName);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && PlayerPrefs.GetInt("SceneManager") == 0)
            {
                PlayerPrefs.SetInt("SceneManager", 1);
                SceneManager.LoadScene("Fallout");
            }
            else if (Input.GetKeyDown(KeyCode.Space) && PlayerPrefs.GetInt("SceneManager") == 1)
            {
                SceneManager.LoadScene("Fallout");
            }
            
        }
        void MenuAnime()
        {
            if (MenuAnimator.GetBool("IsMenuClicked") == false)
            {
               MenuAnimator.SetBool("IsMenuClicked",true);
            }
            else
            {
                MenuAnimator.SetBool("IsMenuClicked", false);
            }
        }
        void SetName()
        {
            SceneManager.LoadScene("Register");
        }
    }
}
