using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;  // Make AudioSource a serialized field
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    bool canShoot = true;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;

    void Update()
    {
        audioSource = GetComponent<AudioSource>();
       DisplayAmmo();
       if (Input.GetMouseButtonDown(0) && canShoot == true)
       {
        StartCoroutine(Shoot());
       } 
    }
    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            // Play the attack sound for 3 seconds
        if (audioSource != null)
        {
            StartCoroutine(PlaySoundForDuration(1f));
        }
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }
    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            // Debug.Log("i hit this thing: " + hit.transform.name);
            CrrateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }
    private void CrrateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact,1);
    }
            IEnumerator PlaySoundForDuration(float duration)
    {
        audioSource.Play();
        Debug.Log("Sound started");
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
        Debug.Log("Sound stopped");
    }
}
