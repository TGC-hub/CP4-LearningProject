using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBullet : BulletScript
{
    [SerializeField] private ParticleSystem bulletPlantEffect;
    [SerializeField] private ParticleSystem bulletBeeEffect;
    [SerializeField] private ParticleSystem bulletTrunkEffect;
    [SerializeField] private GameObject lightFire;
    private bool onLightFire = false;

    private void Start()
    {
        bulletPlantEffect.transform.parent = null;
        bulletBeeEffect.transform.parent = null;
        bulletTrunkEffect.transform.parent = null;
        onLightFire = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            onLightFire = !onLightFire;
            lightFire.SetActive(onLightFire);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BulletPlant"))
        {
            OnParticleBullet(collision.transform.position, bulletPlantEffect);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("BulletTrunk"))
        {
            OnParticleBullet(collision.transform.position, bulletTrunkEffect);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("BulletBee"))
        {
            OnParticleBullet(collision.transform.position, bulletBeeEffect);
            Destroy(collision.gameObject);
        }
    }

    protected void OnParticleBullet(Vector2 pos, ParticleSystem bulletExplosionEffect)
    {
        bulletExplosionEffect.transform.position = pos;
        bulletExplosionEffect.Play();
    }


}
