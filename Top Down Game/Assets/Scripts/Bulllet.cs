using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulllet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")){
            //Damage Enemy
            //Play particle
            Destroy(gameObject);


        }
        else if (other.CompareTag("Wall") || other.CompareTag("Props"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        if (ParticleManager.Instance != null)
            ParticleManager.Instance.PlayBulletDecoy(transform.position);
    }
}
