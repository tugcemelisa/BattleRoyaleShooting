using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks /*IPunObservable*/
{
    public enum Weapons
    {
        None,
        Pistol,
        Rifle,
        MiniGun
    }
    Weapons weapons = Weapons.None;

    [SerializeField] GameObject pistol, rifle, miniGun;
    [SerializeField] AudioSource characterSounds;
    [SerializeField] AudioClip jump;
    [SerializeField] Image pistolUI, rifleUI, miniGunUI, cusror;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] float shiftSpeed = 10f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float movementSpeed = 5f;
    Vector3 direction;
    bool isPistol, isRifle, isMiniGun;
    bool isGrounded = true;
    float currentSpeed;
    float stamina = 5f;
    int health;
    public bool dead;
    TextUpdate textUpdate;
    [SerializeField] GameObject damageUi;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.ChangePlayersList();

        textUpdate = GetComponent<TextUpdate>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = movementSpeed;
        health = 100;
        if (!photonView.IsMine)
        {
            transform.Find("Main Camera").gameObject.SetActive(false);
            transform.Find("Canvas").gameObject.SetActive(false);
            this.enabled = false;
        }
    }
    public void GetDamage(int count)
    {
        photonView.RPC("ChangeHealth", RpcTarget.All, count);
    }
    [PunRPC]
    public void ChangeHealth(int count)
    {
        health -= count;
        textUpdate.SetHealth(health);
        damageUi.SetActive(true);
        Invoke("RemoveDamageUi", 0.1f);
        if (health <= 0 && !dead)
        {
            dead = true;
            anim.SetBool("Die", true);
            transform.Find("Main Camera").GetComponent<ThirdPersonCamera>().isSpectator = true;
            ChooseWeapon(Weapons.None);
            gameManager.ChangePlayersList();
            //gameManager.PlayerDied(gameObject.name);
            this.enabled = false;
        } // değişiklik
    }
    void RemoveDamageUi()
    {
        damageUi.SetActive(false);
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);

        if (direction.x != 0 || direction.z != 0)
        {
            anim.SetBool("Run", true);
            if (!characterSounds.isPlaying && isGrounded)
            {
                characterSounds.Play();
            }
        }
        if (direction.x == 0 && direction.z == 0)
        {
            anim.SetBool("Run", false);
            characterSounds.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            characterSounds.Stop();
            AudioSource.PlayClipAtPoint(jump, transform.position);
            anim.SetBool("Jump", true);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                stamina -= Time.deltaTime;
                currentSpeed = shiftSpeed;
            }
            else
            {
                currentSpeed = movementSpeed;
            }
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            stamina += Time.deltaTime;
            currentSpeed = movementSpeed;
        }
        if (stamina > 5f)
        {
            stamina = 5f;
        }
        else if (stamina < 0)
        {
            stamina = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && isPistol)
        {
            photonView.RPC("ChooseWeapon", RpcTarget.All, Weapons.Pistol);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && isRifle)
        {
            photonView.RPC("ChooseWeapon", RpcTarget.All, Weapons.Rifle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && isMiniGun)
        {
            photonView.RPC("ChooseWeapon", RpcTarget.All, Weapons.MiniGun);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            photonView.RPC("ChooseWeapon", RpcTarget.All, Weapons.None);
        }
    }
    [PunRPC]
    public void ChooseWeapon(Weapons weapons)
    {
        anim.SetBool("Pistol", weapons == Weapons.Pistol);
        anim.SetBool("Assault", weapons == Weapons.Rifle);
        anim.SetBool("MiniGun", weapons == Weapons.MiniGun);
        anim.SetBool("NoWeapon", weapons == Weapons.None);
        pistol.SetActive(weapons == Weapons.Pistol);
        rifle.SetActive(weapons == Weapons.Rifle);
        miniGun.SetActive(weapons == Weapons.MiniGun);

        if (weapons != Weapons.None)
        {
            cusror.enabled = true;
        }
        else
        {
            cusror.enabled = false;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("Jump", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "pistol":
                if (!isPistol)
                {
                    isPistol = true;
                    pistolUI.color = Color.white;
                    ChooseWeapon(Weapons.Pistol);
                }
                break;

            case "rifle":
                if (!isRifle)
                {
                    isRifle = true;
                    rifleUI.color = Color.white;
                    ChooseWeapon(Weapons.Rifle);
                }
                break;

            case "minigun":
                if (!isMiniGun)
                {
                    isMiniGun = true;
                    miniGunUI.color = Color.white;
                    ChooseWeapon(Weapons.MiniGun);
                }
                break;
            default:
                break;
        }
        Destroy(other.gameObject);
    }


}