using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInputs : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private Transform orientation;

    private Vector3 _bodyDirection;
    private Rigidbody _bodyFPSRb; 

    public float groundDrag;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCD;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private GameObject _mesh;

    Collider meshh;

    public KeyCode jumpKey = KeyCode.Space;
    public bool playerGrounded;
    
    [SerializeField] private LayerMask Platforms;

    private bool _readyForJump = true;
    

    float horizontalInput;
    float verticalInput;
    
    public float playerHeight;

    private void Start() {
        _bodyFPSRb = GetComponent<Rigidbody>();
        _bodyFPSRb.freezeRotation = true;
    }

    private void Update()
    {
        SpeedControl();
        MyInput();

        playerGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, Platforms);

        if(playerGrounded) {
            _bodyFPSRb.drag = groundDrag;
            //Debug.Log("Stand!");
        }    
        else {
            _bodyFPSRb.drag = 0;
            //Debug.Log("Jumping!");
        }

        if (Input.GetKey(jumpKey) && _readyForJump && playerGrounded) {
            Debug.Log("Jump!");
            playerJump();

            _readyForJump = false;
            
            Invoke(nameof(ResetJump), _jumpCD);
        }

        if (Input.GetMouseButton(1)){
            meshh = _mesh.GetComponent<Collider>();
            meshh.isTrigger = true;
        }
    }

    private void FixedUpdate() {
        FPSMove();
    }

    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    
    private void FPSMove() {
        _bodyDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(playerGrounded){
        _bodyFPSRb.AddForce(_bodyDirection.normalized * _speed * 10f, ForceMode.Force);
        } 
        else if (!playerGrounded) {
        _bodyFPSRb.AddForce(_bodyDirection.normalized * _speed * 10f * _airMultiplier, ForceMode.Force);
        }   
    }

    private void SpeedControl() {
        Vector3 surfVelocity = new Vector3(_bodyFPSRb.velocity.x, 0f, _bodyFPSRb.velocity.z);

        if(surfVelocity.magnitude > _speed) {
            Vector3 limitedVelocity = surfVelocity.normalized * _speed;
            _bodyFPSRb.velocity = new Vector3(limitedVelocity.x, _bodyFPSRb.velocity.y, limitedVelocity.z);
        }
    }

    private void playerJump() {
        _bodyFPSRb.velocity = new Vector3(_bodyFPSRb.velocity.x, 0f, _bodyFPSRb.velocity.z);

        _bodyFPSRb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        _readyForJump = true;
    }
}
