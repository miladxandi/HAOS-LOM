using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HAOS.Model
{
    public class Register : MonoBehaviour
    {

        public Button Reg,Back;
        private string Response;
        public InputField Firstname, Lastname, Email, Phone,Password;
        // Use this for initialization
        void Start()
        {
            if (PlayerPrefs.GetString("Firstname") != "" && PlayerPrefs.GetString("Lastname") != "" && PlayerPrefs.GetString("Password") != "" && PlayerPrefs.GetString("Email") != "" && PlayerPrefs.GetString("Phone") != "")
            {
                Firstname.text = PlayerPrefs.GetString("Firstname");
                Lastname.text = PlayerPrefs.GetString("Lastname");
                Email.text = PlayerPrefs.GetString("Email");
                Phone.text = PlayerPrefs.GetString("Phone");
                Password.text = PlayerPrefs.GetString("Password");
            }
            Reg.onClick.AddListener(RegisterUser);
            Back.onClick.AddListener(BackToMain);
        }
        public void RegisterUser()
        {
            if (Firstname.text != "" && Lastname.text != "" && Password.text != "" && Email.text != "" && Phone.text != "")
            {
                StartCoroutine(ReqSender());
//                Registration Json = JsonUtility.FromJson<Registration>(Response);
//                if (Response == "1")
//                {
                    PlayerPrefs.SetString("Firstname", Firstname.text);
                    PlayerPrefs.SetString("Lastname", Lastname.text);
                    PlayerPrefs.SetString("Password", Password.text);
                    PlayerPrefs.SetString("Username", Email.text);
                    PlayerPrefs.SetString("Email", Email.text);
                    PlayerPrefs.SetString("Phone", Phone.text);
                    SceneManager.LoadScene("MainSpace");
//                }
//                else
//                {
//                    PlayerPrefs.DeleteAll();
//                }
                
            }
        }

        IEnumerator ReqSender()
        {
            WWWForm form = new WWWForm();
            form.AddField("Username", Email.text);
            form.AddField("Firstname", Firstname.text);
            form.AddField("Lastname", Lastname.text);
            form.AddField("Phone", Phone.text);
            form.AddField("Email", Email.text);
            form.AddField("Password", Password.text);

            using (UnityWebRequest www = UnityWebRequest.Post("http://www.eastcloud.ir/api/auth/register", form))
            {
//                UnityWebRequestAsyncOperation operation = www.SendWebRequest();
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Response = www.downloadHandler.text;
                }
            }
        }
        void BackToMain()
        {
            SceneManager.LoadScene("MainSpace");
        }
        
    }
}
