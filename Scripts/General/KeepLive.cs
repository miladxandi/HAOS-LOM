using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HAOS.General
{
    class KeepLive : MonoBehaviour
    {
        void Awake()
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");
            if (obj.Length > 1)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
