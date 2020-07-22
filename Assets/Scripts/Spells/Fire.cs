using UnityEngine;

public class Fire : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem Particules;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }
    }

    void Shoot()
    {
        Particules.Play();

        RaycastHit hit;
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Ennemy_1 target = hit.transform.GetComponent<Ennemy_1>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }
}
