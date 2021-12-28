using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity =100f ; // velocità del mouse 

    float xRotation = 0f ; 

    public Transform playerBody ; //Prende da inspector un oggetto che vogliamo collegare 
    void Start()
    {
        Cursor.lockState= CursorLockMode.Locked ;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX=Input.GetAxis("Mouse X") *mouseSensitivity * Time.deltaTime; //Time.deltaTime controlla l'ultimo frame registrato , Questo comando prende il movimento asse x del mouse
        float mouseY=Input.GetAxis("Mouse Y") *mouseSensitivity* Time.deltaTime ; // movimento asse y del mouse 
        xRotation -= mouseY ;
        xRotation=Mathf.Clamp(xRotation,-90f,90f) ; 

        transform.localRotation=Quaternion.Euler(xRotation,0f,0f) ; 


        playerBody.Rotate(Vector3.up*mouseX) ; // la visuale del player si vuove verso l'asse delle x 
    }
}
