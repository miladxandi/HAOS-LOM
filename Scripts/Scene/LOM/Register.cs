using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HAOS.Scene.LOM
{
    public class Register : MonoBehaviour
    {

        public Button Reg,Back;
        public InputField Name, Email, Phone;
        // Use this for initialization
        void Start()
        {
            if (PlayerPrefs.GetString("Name")!="" && PlayerPrefs.GetString("Email") != "" && PlayerPrefs.GetString("Phone") != "")
            {
                Name.text = PlayerPrefs.GetString("Name");
                Email.text = PlayerPrefs.GetString("Email");
                Phone.text = PlayerPrefs.GetString("Phone");
            }
        }
        // Update is called once per frame
        void Update()
        {
            Reg.onClick.AddListener(RegisterUser);
            Back.onClick.AddListener(BackToMain);
        }
        void RegisterUser()
        {
            if (Name.text != "" && Email.text != "" && Phone.text != "")
            {
                PlayerPrefs.SetString("Name", Name.text);
                PlayerPrefs.SetString("Email", Email.text);
                PlayerPrefs.SetString("Phone", Phone.text);
                SceneManager.LoadScene("MainSpace");
            }
        }
        void BackToMain()
        {
            SceneManager.LoadScene("MainSpace");
        }
    }
}
