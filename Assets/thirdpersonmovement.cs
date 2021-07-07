
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class thirdpersonmovement : MonoBehaviour {

    public Transform camtrans;

    Vector3 velocityvec3;
    public float Velocity;
    [Space]

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;
    private int jump_count = 0;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public string playeranimationstatus = "idle_normal";
    public string playeranimationspeed = "0,0,0";
    private Vector3 prevpos;
    private float timetoaniend = 0;
    private float jumpstarttime;
    private bool jumpstatus = false;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity; 


     [Header("Animation Smoothing")]
     [Range(0, 1f)]
    public float HorizontalAnimsmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
    private Vector3 moveVector;

    // Use this for initialization
    void Start() {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isGrounded)
        {
            //Debug.Log("grounded");
            jump_count = 0;
            verticalVel -= 0;
            if (jumpstatus == true)
            {
                jumpstatus = false;
                Debug.Log("hell");
                GetComponent<CharacterSkinController>().ChangeAnimatorIdle("normal");
            }

            //Debug.Log("reset jump count");
        }

        isGrounded = controller.isGrounded;


        //if(isGrounded == false){
        if (isGrounded == false)
        {
            //verticalVel -= 0.05f;
            verticalVel -= 0.4f;

        }

        if (jumpstatus == true) {
            verticalVel -= 0.05f;
        }


        if (Input.GetButtonDown("Jump"))
        {
            jump_count = jump_count + 1;


            if (jump_count < 2) {
                Debug.Log("jump");
                jumpstarttime = Time.time;
                jumpstatus = true;
                
                if (jump_count == 1) {
                    anim.SetTrigger("Jump");
                    verticalVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if (jump_count == 2) {
                    if (isGrounded != true) {

                        anim.SetTrigger("Double_Land");
                    }

                    //verticalVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
                //controller.Move(velocityvec3 * Time.deltaTime);           

            }

        }
        InputMagnitude();

        moveVector = new Vector3(0, verticalVel * 2f * Time.deltaTime, 0);
        controller.Move(moveVector);

        //moveVector = new Vector3(0, verticalVel * .1f * Time.deltaTime, 0);
        //controller.Move(moveVector);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camtrans.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (Input.GetKeyDown(KeyCode.LeftShift)){

            Velocity = Velocity * 2;
            

        }

        if (blockRotationPlayer == false) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
        }
    }

    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
        //anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

        //Calculate the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player

        if (Speed > allowPlayerRotation) {
            if (prevpos != this.transform.position) {
                //Debug.Log("jj");
                anim.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
                playeranimationstatus = "running";
                timetoaniend = Time.time;
            }

            if (Time.time - timetoaniend > 0.5) {
                if (playeranimationstatus != "idle_normal") {
                    anim.SetFloat("Blend", 0, 0, 0);
                    playeranimationstatus = "idle_normal";
                }
            }

            //if (prevpos == this.transform.position){{
            //Debug.Log("www");
            //anim.SetFloat ("Blend", 0, 0, 0);
            //}
            //MultiplayerManager.setplayeranimation(playeranimationstatus);
            prevpos = this.transform.position;
            PlayerMoveAndRotation();
        } else if (Speed < allowPlayerRotation) {
            anim.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);
            playeranimationstatus = "idle_normal";
            playeranimationspeed = Speed.ToString() + "," + StartAnimTime.ToString();
            //MultiplayerManager.setplayeranimation(playeranimationstatus);
        }
    }
}  





/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class thirdpersonmovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public int jump_count = 0;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask groundMask;

    public int playernumb = 0;
    public int totalplayernumb = 2;
    public bool once = true;
    bool player_active = false;
    bool game_start_by_player = false;


    Vector3 velocity;
    bool isGrounded;

    void Start()
    {

        player_active = true;
        game_start_by_player = true;
        
    }


    // Update is called once per frame
    void Update()
    {   
        

        
        if (player_active && game_start_by_player)
        {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
            //Debug.Log("Jump");
            //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //}

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }


        if (jump_count < 1)
            {
              //Debug.Log("can jump");
            }

        if (Input.GetButtonDown("Jump"))
        {
            if (jump_count < 1)
            {
              //Debug.Log("can jump and is");
              velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
              jump_count = jump_count + 1;

            }

        }
        
        if (isGrounded)
        {
            //Debug.Log("grounded");
            jump_count = 0;
            //Debug.Log("reset jump count");
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    }
}
*/