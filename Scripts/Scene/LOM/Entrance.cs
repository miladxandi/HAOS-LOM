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
            Debug.Log(PlayerPrefs.GetInt("SceneManager"));
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space) && (PlayerPrefs.GetInt("SceneManager") == 0 || PlayerPrefs.GetInt("SceneManager") == null))
            {
                PlayerPrefs.SetInt("SceneManager", 1);
                SceneManager.LoadScene("Fallout");
            }
            else if (Input.GetKeyDown(KeyCode.Space) && PlayerPrefs.GetInt("SceneManager") == 1)
            {
                SceneManager.LoadScene("Mars1");
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
            if (PlayerPrefs.GetInt("SceneManager") == 0 || PlayerPrefs.GetInt("SceneManager") == null)
            {
                PlayerPrefs.SetInt("SceneManager", 1);
                SceneManager.LoadScene("Fallout");
            }
            else if (PlayerPrefs.GetInt("SceneManager") == 1)
            {
                SceneManager.LoadScene("Mars1");
            }
        }
        void FuncAbout()
        {
            System.Diagnostics.Process.Start("https://eastcloud.ir/en-us");
        }
        void FuncDeveloper()
        {
            System.Diagnostics.Process.Start("https://eastcloud.ir/Haos");
        }
        void FuncContact()
        {
            System.Diagnostics.Process.Start("https://eastcloud.ir/contactus");
        }
    }
}
