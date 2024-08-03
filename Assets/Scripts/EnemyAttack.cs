using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] AudioSource audioSource;  // Make AudioSource a serialized field
    [SerializeField] float damage = 40f;
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnDamageTaken()
    {
        Debug.Log(name + "i also know that we took damage");
    }
    public void AttackHitEvent()
    {
        if (target == null) return;
        target.TakeDamage(damage);
        Debug.Log("bang bang");
        target.GetComponent<DispalyDamage>().ShowDamageImpact();
        // Play the attack sound for 3 seconds
        if (audioSource != null)
        {
            StartCoroutine(PlaySoundForDuration(1f));
        }   }

        IEnumerator PlaySoundForDuration(float duration)
    {
        audioSource.Play();
        Debug.Log("Sound started");
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
        Debug.Log("Sound stopped");
    }
}