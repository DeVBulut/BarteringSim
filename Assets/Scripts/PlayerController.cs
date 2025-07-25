using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private Vector3 playerInput;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float turnSpeed = 360; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        if (playerInput != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(playerInput);

            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        Vector3 targetPosition = (transform.position + (transform.forward * playerInput.magnitude) * playerSpeed * Time.deltaTime);
        rb.MovePosition(targetPosition);
    }
}
