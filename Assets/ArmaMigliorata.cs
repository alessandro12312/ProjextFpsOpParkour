using System.Collections;
using UnityEngine;
using TMPro;

public class ArmaMigliorata : MonoBehaviour
{
    public GameObject Impact;
    //Proiettile 
    public GameObject Bullet;
    //Forza Proiettile 
    public float shootForce, upwareForce;

    //Stato arma
    public float TimeShooting, spread, reloadTime, TimeShoots;
    //Stato caricatore 
    public int SpazioCaricatore, ColpiPerPressione;
    //Controllo se tieni premuto il pulsante 
    public bool MantieniPremuto;
    // Controllo proiettili rimasti e quelli sparati 
    int bulletRimasti, bulletSparati;

    // Controllo di fuoco
    bool spara, Readyfire, reloading;

    public Camera fpsCam;
    public Transform attackPoint;
    //Grafica
    public ParticleSystem muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;


    //bug fix 
    public bool allowInvoke = true;

    private void Awake()
    {
        bulletRimasti = SpazioCaricatore;
        Readyfire = true;
    }

    private void Update()
    {
        MyInput();

        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletRimasti / ColpiPerPressione + "/" + SpazioCaricatore / ColpiPerPressione);
        }
    }


    private void MyInput()
    {
        if (MantieniPremuto) spara = Input.GetKey(KeyCode.Mouse0);
        else spara = Input.GetKeyDown(KeyCode.Mouse0);

        if (Readyfire && spara && !reloading && bulletRimasti > 0)
        {
            bulletSparati = 0;

            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletRimasti < SpazioCaricatore && !reloading)
        {
            Reload();
        }

        if (Readyfire && spara && !reloading && bulletRimasti <= 0)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        Readyfire = false;

        Vector3 targetPoint;
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
            targetPoint = ray.GetPoint(75);

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 DirectionNoSpray = targetPoint - attackPoint.position;
        Vector3 DirezioneSpray = DirectionNoSpray + new Vector3(x, y, 0);

        GameObject Proiettile = Instantiate(Bullet, attackPoint.position, Quaternion.identity);

        Proiettile.transform.forward = DirezioneSpray.normalized;

        Proiettile.GetComponent<Rigidbody>().AddForce(DirezioneSpray.normalized * shootForce, ForceMode.Impulse);
        Proiettile.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwareForce, ForceMode.Impulse);
        GameObject ImpatctGun = Instantiate(Impact, hit.point, Quaternion.LookRotation(hit.normal));

        if (allowInvoke)
        {
            Invoke("ResetShot", TimeShooting);
            allowInvoke = false;
        }

        if (bulletSparati < ColpiPerPressione && bulletRimasti > 0)
        {
            Invoke("Shoot", TimeShoots);
        }
        bulletRimasti--;
        bulletSparati++;
    }

    private void ResetShot()
    {
        Readyfire = true;
        allowInvoke = true;

    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletRimasti = SpazioCaricatore;
        reloading = false;
    }
}
