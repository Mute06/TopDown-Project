using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShooting : MonoBehaviour
{
    [SerializeField] float bulletLifeTime = 2f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Joystick aimJoystick;

    [SerializeField] private Transform shootingPoint;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector3 lastAimDirection;
    private bool isFacingLeft;
    private bool isShooting;

    private void Start() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (shootingPoint == null){
            shootingPoint = transform;
        }
    }

    private void OnEnable() {
        aimJoystick.OnJoystickUpEvent += Shoot;
    }
    private void OnDisable() {
        aimJoystick.OnJoystickUpEvent -= Shoot;
    }

    private void Update() {
        Aim();
    }


    private void Shoot(){
        Vector2 aim = aimJoystick.Direction;
        if (aim.magnitude < 0.1f){
            aim = lastAimDirection;
            if (aim.magnitude < 0.1f)
            {
                aim = Vector2.right;
            }
        }
        else{
             aim.Normalize();
        }

        GameObject bullet = Instantiate(bulletPrefab,shootingPoint.position,Quaternion.identity);
        float angle = Mathf.Atan2(aim.y,aim.x) * Mathf.Rad2Deg;
        bullet.transform.Rotate(0,0f, angle, Space.World);
        Rigidbody2D bulletsRb = bullet.GetComponent<Rigidbody2D>();
        bulletsRb.velocity = aim * bulletSpeed;
        Destroy(bullet,bulletLifeTime);
        if (!isShooting)
        {
            animator.SetTrigger("Fire");
            ParticleManager.Instance.PlayGunSmoke();
            isShooting = true;
        }

    }

    private void Aim(){
        Vector3 aim = new Vector3(aimJoystick.Horizontal, aimJoystick.Vertical, 0f);
        if (!(aim.magnitude > 0.1f)){
            return;
        }
    
        aim.Normalize();
        float angle = Mathf.Atan2(aim.y,aim.x) * Mathf.Rad2Deg;
        if ((angle > 90f || angle < -90f)){
            if (!isFacingLeft){
                // When Turned Left Side
                spriteRenderer.flipY = true;
                isFacingLeft = true;
            }

        }
        else {
            if (isFacingLeft){

                // When Turned Right side
                spriteRenderer.flipY = false;
                isFacingLeft = false;
            }
            
        }
        transform.rotation = Quaternion.Euler(0f,0f,angle);
        lastAimDirection = aim;
        
    }

    public void SetIsShootingToFalse()
    {
        isShooting = false;
    }
}
