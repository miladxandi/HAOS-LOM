using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HAOS.General;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using HAOS.Object;

namespace HAOS.Model
{
    public class Register : MonoBehaviour
    {

        public Button Reg,Back,Login,Logout;
        private string Response;
        public GameObject RegisterPanel, LoginPanel,LoginBtn,RegisterBtn,LogoutBtn;
        public InputField Firstname, Lastname, Email, Phone,Password;
        // Use this for initialization
        void Start()
        {
            if (PlayerPrefs.GetString("Firstname") != "" && PlayerPrefs.GetString("Lastname") != "" && PlayerPrefs.GetString("Email") != "" && PlayerPrefs.GetString("Phone") != "")
            {
                Firstname.text = PlayerPrefs.GetString("Firstname");
                Lastname.text = PlayerPrefs.GetString("Lastname");
                Email.text = PlayerPrefs.GetString("Email");
                Phone.text = PlayerPrefs.GetString("Phone");
                Password.text = "000000";
                
                
                Firstname.readOnly = true;
                Lastname.readOnly = true;
                Email.readOnly = true;
                Phone.readOnly = true;
                Password.readOnly = true;
                
                
                LoginBtn.SetActive(false);
                LogoutBtn.SetActive(true);
                RegisterBtn.SetActive(false);
            }
            Reg.onClick.AddListener(RegisterUser);
            Logout.onClick.AddListener(ClearData);
            Login.onClick.AddListener(ShowLogin);
            Back.onClick.AddListener(BackToMain);
        }
        public void RegisterUser()
        {
            if (Firstname.text != "" && Lastname.text != "" && Password.text != "" && Email.text != "" && Phone.text != "")
            {
                StartCoroutine(Request("https://www.eastcloud.ir/api/auth/register","Firstname="+Firstname.text+"&Lastname="+Lastname.text+"&Password="+Password.text+"&Username="+Email.text+"&Email="+Email.text+"&Phone="+Phone.text));
                RootObject Json = JsonConvert.DeserializeObject<RootObject>(Response);
                if (Json.Status == "Success")
                {
                    PlayerPrefs.SetString("Firstname", Firstname.text);
                    PlayerPrefs.SetString("Lastname", Lastname.text);
                    PlayerPrefs.SetString("Password", Password.text);
                    PlayerPrefs.SetString("Username", Email.text);
                    PlayerPrefs.SetString("Email", Email.text);
                    PlayerPrefs.SetString("Phone", Phone.text);
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

        void ClearData()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Register");
        }
        void ShowLogin()
        {
            LoginPanel.SetActive(true);
            RegisterPanel.SetActive(false);
        }
        
    }
}
