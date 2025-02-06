using UnityEngine;

public class Inv_PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    float currentSpeed;
    Rigidbody rb;
    Vector3 direction;
    float jumpForce = 7f;
    bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = movementSpeed;        
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            isGrounded = false;
            rb.AddForce(new Vector3 (0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
