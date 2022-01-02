
using System.Collections;
using UnityEngine;

public class ArmaScript : MonoBehaviour
{
    public bool FireType= true ;
    public float damage= 10f ;
    public float range = 100f ;

    public float FireRate =15f;    
    public Camera fpsCam;

    public GameObject Impact ; 

    
    public ParticleSystem muzzleFlash ;

    private float RateoFuoco = 0f ;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && FireType == true) 
        {
            FireType = false ; 
        } 

        else if (Input.GetKeyDown(KeyCode.B) && FireType == false) 
        {
            FireType = true ; 
        } 

        if (FireType == false)
        {
            if (Input.GetButton("Fire1")&& Time.time >= RateoFuoco)  
            {
                RateoFuoco = Time.time + 1f / FireRate ; //Messo il fuoco rapido 
                Shoot();
                Debug.Log(FireType);
            }
        }
        if (FireType == true) 
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();  
                 Debug.Log(FireType);          //FuocoSingolo 
            }
        }
    }

    void Shoot()
    {       
        
            muzzleFlash.Play();
            RaycastHit hit;

            if (Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit,range)) 
            {
                Debug.Log(hit.transform.name);

               Target target = hit.transform.GetComponent<Target>();

               if (target!=null) 
               {
                   target.takedmg(damage) ;
               }

               GameObject ImpatctGun= Instantiate(Impact,hit.point,Quaternion.LookRotation(hit.normal)) ;

               Destroy(ImpatctGun,1f) ;
            }
    }
}
