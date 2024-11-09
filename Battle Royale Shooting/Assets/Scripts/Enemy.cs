using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float attackDistance;    
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    protected GameObject player;    
    protected Animator anim;
    protected Rigidbody rb;
    protected float distance;
    protected float timer;  
    bool dead = false;

    public virtual void Move() 
    {
    }
    public virtual void Attack() 
    {
    }
    public void ChangeHealth(int count)
    {
        health -= count;
        if(health <= 0)
        {
            dead = true;
            GetComponent<Collider>().enabled = false;
            anim.SetBool("Die", true);
        }
    }
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject; 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (!dead)
        {            
            Attack();
        }        
    }
    private void FixedUpdate()
    {
        if (!dead)
        {
            Move();
        }
    }
}
