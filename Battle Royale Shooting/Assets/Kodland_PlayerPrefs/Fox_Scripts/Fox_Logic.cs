using TMPro;
using UnityEngine;

public class Fox_Logic : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] TMP_Text coinsText;
    Animator anim;
    Rigidbody rb;    
    Vector3 dir;
    bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        if (PlayerPrefs.HasKey("posX"))
        {
            float loadedX = PlayerPrefs.GetFloat("posX");
            float loadedY = PlayerPrefs.GetFloat("posY");
            float loadedZ = PlayerPrefs.GetFloat("posZ");
            transform.position = new Vector3(loadedX, loadedY, loadedZ);
        }
        if (PlayerPrefs.HasKey("Coins"))
        {
            GetCoin();
        }
    }
    public void GetCoin()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        dir = new Vector3(hor, 0.0f, ver);
        dir = transform.TransformDirection(dir);
        FoxAnim(hor, ver);
        Jump();       
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("Jump", false);
    }
   
    void FoxAnim(float h, float v)
    {
        if (dir.x != 0 || dir.z != 0)
        {
            anim.SetBool("Run", true);
        }
        else if (dir.x == 0 && dir.z == 0)
        {
            anim.SetBool("Run", false);
        }

        if (h <= -0.01f)
        {
            anim.SetFloat("Blend", 0f);
        }
        else if (h >= 0.01f)
        {
            anim.SetFloat("Blend", 1f);
        }
        else if (h < 0.1f && h > -0.1f && v != 0)
        {
            anim.SetFloat("Blend", 0.5f);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            anim.SetBool("Jump", true);
        }
    }
}
