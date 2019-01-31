using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eastCloud.Planets.Movement
{
    public class Rotation : MonoBehaviour
    {

        private GameObject Planet;
        public GameObject Atmosphere, Clouds, Greens;
        public Vector3 Des;
        public float Speed = 0.2f;
        public bool IsRotate = true;
        public bool IsUranus = false;
        // Use this for initialization
        void Start()
        {
            Planet = this.gameObject;
            if (IsRotate == true)
            {


                if (Planet.name == "Uranus" || IsUranus == true)
                {
                    Des.x = Des.y != 0 ? Des.y : Des.y = Speed;
                    Des.y = 0;
                    Des.z = 0;
                }
                else
                {
                    Des.y = Des.y != 0 ? Des.y : Des.y = Speed;
                    Des.x = 0;
                    Des.z = 0;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            Planet.transform.Rotate(Des);
        }
    }
}