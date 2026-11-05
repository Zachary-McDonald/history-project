using System.Collections;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public InputActionAsset inputActions;
    public CharacterController characterController;
    public GameObject playerBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Transform playerCamera;
    private InputAction playerMoveAction;
    private InputAction playerRoll;
   

    private float walkSpeed = 10f;
    private float rotationDampening = 0.1f;
    private float turnSmoothVelocity;

    private float verticalVelocity = 0f;
    private float gravity = -9.81f;
    

    public float attackDuration = 0.1f;
    public float attackCooldown = 1.4f;
    public Animator animator;
   
   

    private Vector2 playermoveAmount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playermoveAmount = playerMoveAction.ReadValue<Vector2>();
        playerMoveAndRotate();
        jump();
        verticalVelocity += gravity * Time.deltaTime;
       

    }
    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();




    }
    private void Awake()
    {
        playerMoveAction = InputSystem.actions.FindAction("Move");
        playerRoll = InputSystem.actions.FindAction("Jump");
       
    }
    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void playerMoveAndRotate()
    {
        Vector3 playerDirection = new Vector3(playermoveAmount.x, 0, playermoveAmount.y).normalized;
        Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime;
        animator.SetFloat("speed", playerDirection.magnitude);
      
       

        if (playerDirection.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(playerDirection.x, playerDirection.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float smoothTargetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationDampening);

            transform.rotation = Quaternion.Euler(0f, smoothTargetAngle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            
            characterController.Move(moveDirection.normalized * walkSpeed * Time.deltaTime + verticalMove);
            

        }
        else
        {
            characterController.Move(verticalMove);
            
        }
    }
    private void jump()
    {
        if (characterController.isGrounded)
        {
            
            if (playerRoll.WasPressedThisFrame())
            {
                animator.SetBool("rolling", true);
                StartCoroutine(Attack());


            }
        }
    }
    private IEnumerator Attack()
    {
        Debug.Log("start");
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("rolling", false);

        
    }


}
