using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Joystick aimJoystick;
    [SerializeField] private float distanceMultipleir;
    private SpriteRenderer spriteRenderer;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        MoveCrosshair();
    }

    private void MoveCrosshair(){
        Vector3 aim = new Vector3(aimJoystick.Horizontal, aimJoystick.Vertical, 0f);

        if (aim.magnitude > 0.0f){
            spriteRenderer.enabled = true;
            aim.Normalize();
            aim *= distanceMultipleir;
            transform.localPosition = aim;
        }
        else{
            spriteRenderer.enabled = false;
        }
    }
}
