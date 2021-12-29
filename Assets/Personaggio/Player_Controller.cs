using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{   
    //movimento player
    public CharacterController controller;
    public float speed = 12f ;

    //Gravita
    public float gravity=-9.81f ;


    public Transform Terra ; 
    public float distanzaTerra = 0.4f;

    public LayerMask TerraMask ;
    bool eTerra ;

    public float altezzaSalto=3f;

    Vector3 velocita ;
    //
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //movimento del player
        float x = Input.GetAxis("Horizontal") ;
        float z =Input.GetAxis("Vertical") ; 

        Vector3 move = transform.right * x + transform.forward * z ; 

        controller.Move(move*speed*Time.deltaTime) ;

        if(Input.GetButtonDown("Jump")&& eTerra)
        {
            velocita.y =Mathf.Sqrt(altezzaSalto*-2f*gravity);
        }

        //Controllo gravità
        eTerra = Physics.CheckSphere(Terra.position,distanzaTerra,TerraMask) ;

        if(eTerra && velocita.y<0)
        {
            velocita.y =-1f ;
        }

        velocita.y += gravity * Time.deltaTime ; 


        controller.Move(velocita * Time.deltaTime);
    }
}
