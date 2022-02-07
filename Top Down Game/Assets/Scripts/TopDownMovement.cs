using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Joystick mobileInput;
    private Animator animator;
    private float Input_Horizontal;
    private float Input_Vertical;
    private bool isMoving;

    private int animParam_horizontal;
    private int animParam_vertical;
    private int animParam_isWalking;
    private Rigidbody2D rb;
    private Vector3 finalMoveVector;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animParam_horizontal = Animator.StringToHash("horizontal");
        animParam_vertical = Animator.StringToHash("vertical");
        animParam_isWalking = Animator.StringToHash("isWalking");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        

        Input_Horizontal = mobileInput.Horizontal;
        Input_Vertical = mobileInput.Vertical;

        isMoving = !((Input_Horizontal > -0.1f && Input_Horizontal < 0.1f) && (Input_Vertical > -0.1f && Input_Vertical < 0.1f));

        //Movement
        Vector3 move = new Vector3(Input_Horizontal, Input_Vertical, 0f);
        //transform.position += move * Time.deltaTime * speed;
        //transform.Translate(move * Time.deltaTime * speed);
        finalMoveVector = move * speed;


        //Animator parameters
        if (isMoving)
        {
            SetAnimatorParams();
        }
        animator.SetBool(animParam_isWalking, isMoving);
    }
    private void FixedUpdate() {
        rb.MovePosition(transform.position + finalMoveVector);
    }

    private void SetAnimatorParams()
    {
        animator.SetFloat(animParam_horizontal, Input_Horizontal);
        animator.SetFloat(animParam_vertical, Input_Vertical);
    }
}
