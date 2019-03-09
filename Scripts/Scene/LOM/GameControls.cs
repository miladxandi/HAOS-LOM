using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace HAOS.Scene.LOM
{
    public class GameControls : MonoBehaviour
    {

        public Rigidbody Player;
        public Camera MainCamera;
        public Collider Dust, NotSun;
        public GameObject WalkDust;
        public Collider[] ChargeReducers = null;
        public Animator Status;
        public Canvas Canv;
        public Text Result, ChargeText;
        private AudioSource DustSound1;
        public Button Send;
        public Image Map;
        private Vector3 Des, rTurn, Turn;
        public float Charge = 10000;
        private float Speed = 2, MRight, MForward, RCam, RRoverRight;
        private bool LetCharge = true, CamRotateRight, RoverRotateRight, RightMove, ForwardMove, EasyMode;
        public InputField Command;
        public float moveSpeed = 10f;
        public float turnSpeed = 50f;

        void Start()
        {
            DustSound1 = Dust.GetComponent<AudioSource>();
            ChargeText.text = "Charge: " + Charge + " kAh";
            Send.onClick.AddListener(ValueSend);
            WalkDust.GetComponent<ParticleSystem>().Stop();
            EasyMode = true;
            Player = GetComponent<Rigidbody>();
            if (EasyMode)
            {
                Command.gameObject.SetActive(false);
                Send.gameObject.SetActive(false);
            }
        }
        void FixedUpdate()
        {
            Charger();
            LetCharge = true;
            if (Charge >= 0)
            {
                if (Charge > 1)
                {
                    if (EasyMode)
                    {

                        if (CheckDisConnection())
                        {
                            Result.text = "Rover Disconnected!";
                        }
                        else
                        {
                            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                            {
                                float moveHorizontal = Input.GetAxis("Horizontal");
                                float moveVertical = Input.GetAxis("Vertical");
                                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                                //Player.AddForce(movement * Speed);
                                transform.Translate(movement * Speed * Time.deltaTime, Space.World);
                                LetCharge = false;
                                Result.text = "Moving Rover!";
                            }
                            if (Input.GetKey(KeyCode.Q))
                            {
                                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                                LetCharge = false;
                                Result.text = "Turning Rover!";
                            }
                            if (Input.GetKey(KeyCode.E))
                            {
                                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                                LetCharge = false;
                                Result.text = "Turning Rover!";
                            }
                        }

                    }
                    else
                    {
                        if (CheckDisConnection())
                        {
                            Command.text = "Rover Disconnected!";
                        }
                        else
                        {
                            SetDisConnection(false);
                            StartCoroutine(Mover());
                            StartCoroutine(Rotator());
                        }

                    }
                }
                else
                {
                    Result.text = "Requires at least 1 kAh charge!";
                }

            }
            else
            {
                SetDisConnection(true);
            }
        }
        void Update()
        {
            StartCoroutine(CameraRotator());
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.H))
            {
                if (Canv.isActiveAndEnabled)
                {
                    Canv.enabled = false;
                }
                else
                {
                    Canv.enabled = true;
                }
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.M))
            {
                if (Map.isActiveAndEnabled)
                {
                    Map.enabled = false;
                }
                else
                {
                    Map.enabled = true;
                }
            }
        }
        public void ValueSend()
        {
            Result.text = "Result:\nSending ...";
            StartCoroutine(waiter());
        }
        public object Resulter(string Command = null)
        {
            string[] SeparatedCommand = Command.Split(new char[] { ' ' }, StringSplitOptions.None);

            if (CheckDisConnection())
            {
                return "Rover Disconnected!";
            }
            else
            {
                if (SeparatedCommand[0].ToLower() == "go")
                {
                    if (int.Parse(SeparatedCommand[2]) <= 100)
                    {
                        switch (SeparatedCommand[1].ToLower())
                        {
                            case "forward":
                                Des.z -= float.Parse(SeparatedCommand[2]);
                                ForwardMove = true;
                                MForward = 0 - Des.z;
                                return "Going forward";
                            case "backward":
                                Des.z += float.Parse(SeparatedCommand[2]);
                                ForwardMove = false;
                                MForward = Des.z;
                                return "Going backward";
                            case "right":
                                Des.x -= float.Parse(SeparatedCommand[2]);
                                RightMove = true;
                                MRight = 0 - Des.x;
                                return "Going right";
                            case "left":
                                Des.x += float.Parse(SeparatedCommand[2]);
                                RightMove = false;
                                MRight = Des.x;
                                return "Going left";
                            default:
                                return "Out of my understanding";
                        }
                    }
                    else
                    {
                        return "Over than 100 exception!";
                    }
                }
                else if (SeparatedCommand[0].ToLower() == "turn")
                {

                    if (SeparatedCommand[1].ToLower() == "rover")
                    {
                        if (int.Parse(SeparatedCommand[3]) <= 300)
                        {
                            switch (SeparatedCommand[2].ToLower())
                            {
                                case "right":
                                    rTurn.y += float.Parse(SeparatedCommand[3]);
                                    RRoverRight = rTurn.y;
                                    RoverRotateRight = true;
                                    return "Turning Right Rover";
                                case "left":
                                    rTurn.y -= float.Parse(SeparatedCommand[3]);
                                    RRoverRight = 0 - rTurn.y;
                                    RoverRotateRight = false;
                                    return "Turning Left Rover";
                                default:
                                    return "Out of my understanding";
                            }
                        }
                        else
                        {
                            return "Over than 100 exception!";
                        }
                    }
                    else
                    {
                        if (int.Parse(SeparatedCommand[2]) <= 500)
                        {
                            switch (SeparatedCommand[1].ToLower())
                            {
                                case "right":
                                    Turn.y += float.Parse(SeparatedCommand[2]);
                                    RCam = Turn.y;
                                    CamRotateRight = true;
                                    return "Turning Right";
                                case "left":
                                    Turn.y -= float.Parse(SeparatedCommand[2]);
                                    RCam = 0 - Turn.y;
                                    CamRotateRight = false;
                                    return "Turning Left";
                                default:
                                    return "Out of my understanding";

                            }
                        }
                        else
                        {
                            return "Over than 100 exception!";
                        }
                    }
                }
                else
                {
                    return "Out of my understanding";
                }
            }

        }
        IEnumerator Mover()
        {
            yield return new WaitForSeconds(3f);
            Vector3 Movement = new Vector3(Des.x, Des.y, Des.z);
            WalkDust.GetComponent<ParticleSystem>().Play();
            if (ForwardMove)
            {
                if (Des.z < 0)
                {
                    //float step = Speed * Time.deltaTime / 10;
                    //transform.position = Vector3.MoveTowards(transform.position, Player.position * Des.x, step);
                    Player.transform.position += Movement * Speed * Time.deltaTime / 10;
                    Charger(false);
                    Des.z += 1f;
                }

            }
            else
            {
                if (Des.z > 0)
                {
                    Player.transform.position += Movement * Speed * Time.deltaTime / 10;
                    Charger(false);
                    Des.z -= 1f;

                }
            }
            if (RightMove)
            {
                if (Des.x < 0)
                {
                    Player.transform.position += Movement * Speed * Time.deltaTime / 10;
                    Charger(false);
                    Des.x += 1f;
                }
            }
            else
            {
                if (Des.x > 0)
                {
                    Player.transform.position += Movement * Speed * Time.deltaTime / 10;
                    Charger(false);
                    Des.x -= 1f;
                }
            }
            WalkDust.GetComponent<ParticleSystem>().Stop();
        }
        IEnumerator Rotator()
        {
            yield return new WaitForSeconds(3f);
            WalkDust.GetComponent<ParticleSystem>().Play();
            if (RoverRotateRight)
            {
                if (rTurn.y > 0)
                {

                    Vector3 Rotation = new Vector3(rTurn.x, rTurn.y, rTurn.z);
                    Player.transform.Rotate(Rotation * (Time.deltaTime / 10));
                    Charger(false);
                    rTurn.y -= 1f;

                }
            }
            else
            {
                if (rTurn.y < 0)
                {
                    Vector3 Rotation = new Vector3(rTurn.x, rTurn.y, rTurn.z);
                    Player.transform.Rotate(Rotation * (Time.deltaTime / 10));
                    Charger(false);
                    rTurn.y += 1f;
                }
            }
            WalkDust.GetComponent<ParticleSystem>().Stop();
        }
        IEnumerator CameraRotator()
        {
            yield return new WaitForSeconds(3f);
            if (CamRotateRight)
            {
                if (Turn.y > 0)
                {
                    Vector3 Rotation = new Vector3(Turn.x, Turn.y, Turn.z);
                    MainCamera.transform.Rotate(Rotation * (Time.deltaTime / 10));
                    Charger(false);
                    Turn.y -= 1f;
                }
            }
            else
            {
                if (Turn.y < 0)
                {
                    Vector3 Rotation = new Vector3(Turn.x, Turn.y, Turn.z);
                    MainCamera.transform.Rotate(Rotation * (Time.deltaTime / 10));
                    Charger(false);

                    Turn.y += 1f;
                }
            }
        }
        void Charger(bool Increase = true)
        {
            if (Increase)
            {
                if (LetCharge)
                {
                    if (Charge < 10000)
                    {
                        Charge += 0.001f;
                        ChargeText.text = "Charge: " + Charge + " kAh";
                        if (Charge >= 1)
                        {
                            SetDisConnection(false);
                        }
                    }
                }
                else
                {
                    if (Charge > 0)
                    {
                        Charge -= 0.05f;
                        ChargeText.text = "Charge: " + Charge + " kAh";

                    }

                }
            }
            else
            {
                Charge -= 0.05f;
                ChargeText.text = "Charge: " + Charge + " kAh";
            }

        }
        IEnumerator waiter()
        {
            yield return new WaitForSeconds(3f);
            Result.text = "Result:\n" + Resulter(Command.text);
            Command.text = null;
        }
        public bool CheckDisConnection()
        {
            return Status.GetBool("DC");
        }
        public void SetDisConnection(bool Disconnection)
        {
            Status.SetBool("DC", Disconnection);
        }
        private void OnTriggerExit(Collider other)
        {
            if (other == Dust)
            {
                SetDisConnection(false);
                DustSound1.Stop();
                LetCharge = true;
                Result.text = "Result:\nConnection established successfully!";

            }
            if (other.tag == "NotSun")
            {
                LetCharge = true;
                Result.text = "Result:\nPower is charging...";

            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other == Dust)
            {
                SetDisConnection(true);
                DustSound1.Play();
                LetCharge = false;
                Result.text = "Result:\nDust storm disconnected rover!";


            }
            if (other.tag == "NotSun")
            {
                LetCharge = false;
                Result.text = "Result:\nWe are losing power in this area!";

            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other == Dust)
            {
                SetDisConnection(true);
                LetCharge = false;
                DustSound1.Play();
            }

            if (other.tag == "NotSun")
            {
                LetCharge = false;
            }
        }
    }
}
