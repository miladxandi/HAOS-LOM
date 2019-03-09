using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using HAOS.Object;

namespace HAOS.Model
{
    public class Login : MonoBehaviour
    {

        public Button Log,Register,Back;
        private string Response;
        public GameObject RegisterPanel, LoginPanel;
        public InputField  Email,Password;
        // Use this for initialization
        void Start()
        {
            if ( PlayerPrefs.GetString("Password") != "" && PlayerPrefs.GetString("Email") != "")
            {
                Email.text = PlayerPrefs.GetString("Email");
                Password.text = PlayerPrefs.GetString("Password");
            }
            Log.onClick.AddListener(LoginUser);
            Back.onClick.AddListener(BackToMain);
            Register.onClick.AddListener(ShowRegister);
        }
        public void LoginUser()
        {
            if (Password.text != "" && Email.text != "")
            {
                
                StartCoroutine(Request("https://www.eastcloud.ir/api/auth/login","&Password="+Password.text+"&Username="+Email.text));
                RootObject Json = JsonConvert.DeserializeObject<RootObject>(Response);

                if (Json.Status == "Success")
                {
                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.SetString("Firstname", Json.Result.Firstname);
                    PlayerPrefs.SetString("Lastname", Json.Result.Lastname);
                    PlayerPrefs.SetString("Email", Json.Result.Email);
                    PlayerPrefs.SetString("Profile", Json.Result.Profile);
                    PlayerPrefs.SetString("Username", Json.Result.Username);
                    PlayerPrefs.SetString("Phone", Json.Result.Phone);
                    SceneManager.LoadScene("MainSpace");
                }
                else
                {
                    PlayerPrefs.DeleteAll();
                }
            }
        }

        private IEnumerator Request(string Host,string Parameters,string Method = "POST")
        {
            WebRequest Req = WebRequest.Create(Host+"?"+Parameters);
            Req.Method = Method;    
            
            WebResponse Res = (HttpWebResponse)Req.GetResponse ();
            Stream dataStream = Res.GetResponseStream ();
            StreamReader reader = new StreamReader (dataStream);
            Response = reader.ReadToEnd();
            
            
            yield return Response;
            reader.Close ();
            dataStream.Close ();
            Res.Close ();
        }
        void BackToMain()
        {
            SceneManager.LoadScene("MainSpace");
        }
        void ShowRegister()
        {
            RegisterPanel.SetActive(true);
            LoginPanel.SetActive(false);
        }
        
    }
}
