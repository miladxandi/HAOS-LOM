using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;

namespace HAOS.General
{
    public class SendRequest:MonoBehaviour
    {
        public string Response;

        private void Start()
        {
            throw new System.NotImplementedException();
        }

        public SendRequest(string Host,string Parameters,string Method = "POST")
        {
                StartCoroutine(Request(Host,Parameters,Method));
        }
        private IEnumerator Request(string Host,string Parameters,string Method = "POST")
        {
            WebRequest Req = WebRequest.Create(Host+"?"+Parameters);
            Req.Method = Method;
  
            
            WebResponse  Res = (HttpWebResponse)Req.GetResponse ();
            Stream dataStream = Res.GetResponseStream ();
            StreamReader reader = new StreamReader (dataStream);
            Response = reader.ReadToEnd();
            
            
            yield return Response;
            reader.Close ();
            dataStream.Close ();
            Res.Close ();
        }
    }
}