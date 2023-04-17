using UnityEngine;
using TMPro;

public class GunV2 : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public int ammo = 100;
    [SerializeField] TextMeshProUGUI ammoDisplay;

    public Camera fpsCam;

    public AudioSource shotSource;

    public Transform muzzle;
    public TrailRenderer Trail;

    // SHOUTOUT NAAR BRACKEYS ALS ZIJ NIET BESTONDEN ZOU IK NOOIT EEN WERKEND WAPEN HEBBEN

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = ammo.ToString();

        Debug.DrawRay(transform.position, transform.forward);
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0)
            {
                Shoot();
                shotSource.Play();
            }
            else if (ammo == 0)
            {

            }
        }
    }

    void Shoot()
    {
        ammo--;
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            var bullet = Instantiate(Trail, muzzle.position, Quaternion.identity);
            bullet.AddPosition(muzzle.position);
            {
                bullet.transform.position = transform.position + (fpsCam.transform.forward * 200);
            }

            bullet.transform.position = hit.point;

            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}