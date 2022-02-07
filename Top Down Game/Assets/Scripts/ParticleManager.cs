using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletDecoyEffect;
    [SerializeField] private ParticleSystem gunSmoke;

    #region Singleton
    public static ParticleManager Instance { get; private set; }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    #endregion

    public void PlayBulletDecoy(Vector3 position){
        Instantiate(bulletDecoyEffect,position,Quaternion.identity,transform);
    }
    
    public void PlayGunSmoke()
    {
        gunSmoke.Play();
    }



}
