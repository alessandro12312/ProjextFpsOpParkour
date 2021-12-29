
using System.Collections;
using UnityEngine;

public class ArmaScript : MonoBehaviour
{
    public float damage= 10f ;
    public float range = 100f ;

    public float FireRate =15f;    
    public Camera fpsCam;

    public GameObject Impact ; 

    
    public ParticleSystem muzzleFlash ;
    IEnumerator Ritardo()
    {
    {
        yield return new WaitForSeconds(.1f);
                Shoot();
    }
    }

    private float RateoFuoco = 0f ;

    void Update()
    {
        if (Input.GetButton("Fire1")&& Time.time >= RateoFuoco)  
        {
            RateoFuoco = Time.time + 1f / FireRate ; 
            StartCoroutine(Ritardo());
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
