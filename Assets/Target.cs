
using UnityEngine;

public class Target : MonoBehaviour
{
    public float healt = 100f ; 

    public void takedmg(float dmg)
    {
        healt-=dmg;

        if (healt<=0f) 
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject) ; 
    }
}
