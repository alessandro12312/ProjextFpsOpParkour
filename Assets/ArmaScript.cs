
using System.Collections;
using UnityEngine;

public class ArmaScript : MonoBehaviour
{
    public float damage= 10f ;
    public float range = 100f ;

    public Camera fpsCam;
    
    public ParticleSystem muzzleFlash ;
    IEnumerator Ritardo()
    {
    {
        yield return new WaitForSeconds(.1f);
                Shoot();
    }
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
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
            }
    }
}
