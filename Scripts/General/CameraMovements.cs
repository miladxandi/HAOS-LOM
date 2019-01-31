using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace HAOS.General
{
    public class CameraMovements : MonoBehaviour
    {

	    public Rigidbody Player;
	    public Camera MainCamera;
	    public Collider Dust,NotSun;
	    public Collider[] ChargeReducers=null;
	    public Animator Status;
	    public Canvas Canv;
	    public Text Result,ChargeText;
	    private AudioSource DustSound1;
	    public Button Send;
        public Image Map,RoverPoint;
	    private Vector3 Des,rTurn,Turn;
	    private float Charge = 1000, Speed=1,MRight, MForward, RCam, RRoverRight;
	    private bool LetCharge = true,CamRotateRight,RoverRotateRight,RightMove,ForwardMove;
	    public InputField Command;

    	void Start ()
	    {
		    DustSound1 = Dust.GetComponent<AudioSource>();
            ChargeText.text = "Charge: " + Charge + " ka";
            Send.onClick.AddListener(ValueSend);
	    }

	    void FixedUpdate()
	    {
            StartCoroutine(Mover());
            StartCoroutine(Rotator());
		    Charger();

		    if (Charge == 0 || Charge <= 0)
		    {
                RoverPoint.enabled = false;
                SetConnection(true);   
		    }
		    else
		    {
                RoverPoint.enabled = false;
                SetConnection(false);   
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
                    RoverPoint.enabled = false;
                }
                else
                {
                    Map.enabled = true;
                    RoverPoint.enabled = true;
                }
            }
        }
	    public void ValueSend()
	    {
		    Result.text = "Result:\nSending ...";
		    StartCoroutine(waiter());
	    }

	    public object Resulter(string Command)
	    {
		    string[] SeparatedCommand = Command.Split(new char[]{' '}, StringSplitOptions.None);

		    if (CheckConnection())
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
                                print(Des.z);
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
                        if (int.Parse(SeparatedCommand[3]) <= 100)
                        {
                            switch (SeparatedCommand[2].ToLower())
                            {
                                case "right":
                                    rTurn.y += float.Parse(SeparatedCommand[3]);
                                    RRoverRight = rTurn.y;
                                    RoverRotateRight = true;
                                    return "Turning Right Rover";
                                    break;
                                case "left":
                                    rTurn.y -= float.Parse(SeparatedCommand[3]);
                                    RRoverRight = 0 - rTurn.y;
                                    RoverRotateRight = false;
                                    return "Turning Left Rover";
                                    break;
                                default:
                                    return "Out of my understanding";
                                    break;

                            }
                        }
                        else
                        {
                            return "Over than 100 exception!";
                        }
                    }
                    else
                    {
                        if (int.Parse(SeparatedCommand[2]) <= 100)
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

            if (ForwardMove)
            {

                if(Des.z <0)
                {
                    //float step = Speed * Time.deltaTime / 10;
                    //transform.position = Vector3.MoveTowards(transform.position, Player.position * Des.x, step);
                    Player.transform.position += Movement * Speed * Time.deltaTime/10;
                    RoverPoint.transform.position += Movement * Speed * Time.deltaTime / 10;
                    Charger(false);
                    Des.z += 1f;
                }

            }
            else
            {
                if (Des.z > 0)
                {
                    Player.transform.position += Movement * Speed * Time.deltaTime / 10;
                    RoverPoint.transform.position += Movement * Speed * Time.deltaTime / 10;
                    Charger(false);
                    Des.z -= 1f;

                }
            }
            if (RightMove)
            {
                if (Des.x < 0)
                {
                    Player.transform.position += Movement * Speed * Time.deltaTime / 10;
                    RoverPoint.transform.position += Movement * Speed * Time.deltaTime / 10;
                    Charger(false);
                    Des.x += 1f;
                }
            }
            else
            {
                if (Des.x > 0)
                {
                    Player.transform.position += Movement * Speed * Time.deltaTime / 10;
                    RoverPoint.transform.position += Movement * Speed * Time.deltaTime / 10;
                    Charger(false);
                    Des.x -= 1f;
                }
            }

	    }
        IEnumerator Rotator()
        {
            yield return new WaitForSeconds(3f);

            if (RoverRotateRight)
            {
                if (rTurn.y > 0)
                {

                    Vector3 Rotation = new Vector3(0, rTurn.y, 0);
                    RoverPoint.transform.Rotate(Rotation * Speed * Time.deltaTime / 10);
                    Charger(false);
                    rTurn.y -= 1f;

                }
            }
            else
            {
                if (rTurn.y < 0)
                {
                    Vector3 Rotation = new Vector3(0, rTurn.y,0);
                    RoverPoint.transform.Rotate(Rotation * Speed * Time.deltaTime / 10);
                    Charger(false);
                    rTurn.y += 1f;
                }
            }

        }
        IEnumerator CameraRotator()
	    {
            yield return new WaitForSeconds(3f);

            if (CamRotateRight)
            {
                if (Turn.y > 0)
                {
                    Vector3 Rotation = new Vector3(Turn.x, Turn.y, Turn.z);
                    Player.transform.Rotate(Rotation * (Time.deltaTime / 10));
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
                    if (Charge < 1000)
                    {
                        Charge += 0.1f;
                        ChargeText.text = "Charge: " + Charge + " ka";
                    }
                }
                else
                {
                    if (Charge > 0)
                    {
                        Charge -= 0.7f;
                        ChargeText.text = "Charge: " + Charge + " ka";

                    }

                }
            }
            else
            {
                Charge = Charge - 0.7f;
                ChargeText.text = "Charge: " + Charge + " ka";
            }

        }

        IEnumerator waiter()
	    {
            yield return new WaitForSeconds(3f);
            Result.text = "Result:\n" + Resulter(Command.text);
            Command.text = null;
        }

        public bool CheckConnection()
	    {
		    return Status.GetBool("DC");
	    }
	    public void SetConnection(bool Disconnection)
	    {
		    Status.SetBool("DC",Disconnection);
	    }

	    private void OnTriggerExit(Collider other)
	    {
		    if (other == Dust)
		    {
			    SetConnection(false);
			    DustSound1.Stop();
                RoverPoint.enabled = true;
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
			    SetConnection(true);
			    DustSound1.Play();
                RoverPoint.enabled = false;
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
			    SetConnection(true); 
			    DustSound1.Play();

		    }

		    if (other.tag == "NotSun")
		    {
			    LetCharge = false;
		    }
	    }
    }
}
