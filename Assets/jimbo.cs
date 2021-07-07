
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class jimbo : MonoBehaviour {

    [Space]

	public Animator anim;
	public CharacterController controller;
	public bool isGrounded;



    public float verticalVel;
    private Vector3 moveVector;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		
		controller = this.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		

        isGrounded = controller.isGrounded;
        if (isGrounded == false)
        {
            //verticalVel -= 0.05f;
            verticalVel -= 0.4f;

        }


        moveVector = new Vector3(0, verticalVel * 2f * Time.deltaTime, 0);
        //controller.Move(moveVector);
        //controller.Move(moveVector);

        //GRAV  
    }


	
}
