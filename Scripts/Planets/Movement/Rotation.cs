using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HAOS.Planets.Movement
{
    public class Rotation : MonoBehaviour
    {

        private GameObject Planet;
        public GameObject Atmosphere, Clouds, Greens;
        public Vector3 Des;
        public bool IsRotate = true;
        // Use this for initialization
        void Start()
        {
            Planet = this.gameObject;
            if (IsRotate == true)
            {


                if (Planet.name == "Uranus")
                {
                    Des.x = Des.y != 0 ? Des.y : Des.y = 0.2f;
                    Des.y = 0;
                    Des.z = 0;
                }
                else
                {
                    Des.y = Des.y != 0 ? Des.y : Des.y = 0.2f;
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