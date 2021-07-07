using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CameraManager : MonoBehaviour
{


public CinemachineFreeLook freeLook1; 
//public CinemachineFreeLook freeLook2;
//public bool once = true; 
//public Camera cam1; 
//public Camera cam2; 
//public Camera cam3; 
 
void Start() {
    //Cursor.visible = false;
    
    freeLook1.m_Priority = 20;
    //cam1.enabled = false;
    //cam2.enabled = false;
    //cam3.enabled = true;
}
 
void Update() {
    //Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;  
    //Cursor.lockState = CursorLockMode.None;  
        
} 
 
// void Update() {
 
      //if (Input.GetKeyDown(KeyCode.Return)) {
        //playernumb = playernumb - 1;

        //game_start_by_player = true;
        //once = false;
        ////
        //freeLook.m_Priority = 20 + 1;
       // }

   //if (once) {
     //if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
     
     //cam1.enabled = true;
     //cam2.enabled = false;

       // freeLook1.m_Priority = 20;
       // freeLook2.m_Priority = 10;

     //cam3.enabled = false;
     //   }
     //else if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
     //cam1.enabled = false;
        //freeLook1.m_Priority = 10;
        //freeLook2.m_Priority = 20;
     //freeLook.m_Priority = freeLook.m_Priority + 1;
     //cam2.enabled = true;
     //cam3.enabled = true;
        }
     /*
     else if (Input.GetKeyDown(KeyCode.C)) {
     cam1.enabled = true;
     cam2.enabled = false;
     cam3.enabled = false;
        }
     
    */
    //}
   //}
//}
