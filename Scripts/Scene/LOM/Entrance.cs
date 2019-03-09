using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace HAOS.Scene.LOM
{
    public class Entrance : MonoBehaviour
    {
        public GameObject MainCamera,Planet;
        public Image Menu,OverlayLayer;
        public Text PlayerName,StartGame;
        public Animator MenuAnimator;
        public Button About,Developer,Contact;

        // Use this for initialization
       
        void Start()
        {
            MenuAnimator = MainCamera.GetComponent<Animator>();
            PlayerName.text = string.IsNullOrEmpty(PlayerPrefs.GetString("Firstname"))!=true ? "Eng. "+PlayerPrefs.GetString("Firstname")+" "+PlayerPrefs.GetString("Lastname") : "ENTER YOUR NAME";
            Menu.GetComponent<Button>().onClick.AddListener(MenuAnime);
            PlayerName.GetComponent<Button>().onClick.AddListener(SetName);
            StartGame.GetComponent<Button>().onClick.AddListener(Launch);
            About.GetComponent<Button>().onClick.AddListener(FuncAbout);
            Contact.GetComponent<Button>().onClick.AddListener(FuncContact);
            Developer.GetComponent<Button>().onClick.AddListener(FuncDeveloper);
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (PlayerPrefs.GetInt("SceneManager") == 0)
                {
                    PlayerPrefs.SetInt("SceneManager", 1);
                    SceneManager.LoadScene("Fallout");

                }
                else if (PlayerPrefs.GetInt("SceneManager") == 1)
                {
                    PlayerPrefs.DeleteKey("SceneLoad");
                    PlayerPrefs.SetString("SceneLoad", "Mars1");
                    SceneManager.LoadScene("Loading");
                }
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
        void Launch()
        {
            if (PlayerPrefs.GetInt("SceneManager") == 0)
            {
                PlayerPrefs.SetInt("SceneManager", 1);
                SceneManager.LoadScene("Fallout");
            }
            else if (PlayerPrefs.GetInt("SceneManager") == 1)
            {
                PlayerPrefs.DeleteKey("SceneLoad");
                PlayerPrefs.SetString("SceneLoad", "Mars1");
                SceneManager.LoadScene("Loading");
            }
        }
        void FuncAbout()
        {
            Application.OpenURL("https://eastcloud.ir");
        }
        void FuncDeveloper()
        {
            Application.OpenURL("https://eastcloud.ir/HAOS/Landing-on-mars");
        }
        void FuncContact()
        {
            Application.OpenURL("https://eastcloud.ir/contactus");
        }
    }
}
