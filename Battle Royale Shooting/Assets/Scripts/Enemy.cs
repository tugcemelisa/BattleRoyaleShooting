using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviourPunCallbacks
{
    [SerializeField] protected int health;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    protected GameObject player;

    protected GameObject[] players;
    //protected GameObject closestPlayer;

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
    public void GetDamage(int count)
    {
        photonView.RPC("ChangeHealth", RpcTarget.All, count);
    }
    [PunRPC]
    public void ChangeHealth(int count)
    {
        health -= count;
        if (health <= 0)
        {
            dead = true;
            GetComponent<Collider>().enabled = false;
            anim.enabled = true;
            anim.SetBool("Die", true);
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        CheckPlayers();
    }
    void CheckPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Invoke("CheckPlayers", 3f);
    }
    private void Update()
    {
        float closestDistance = Mathf.Infinity;
        foreach (GameObject closestPlayer in players)
        {
            float checkDistance = Vector3.Distance(closestPlayer.transform.position, transform.position);
            if (checkDistance < closestDistance)
            {
                if (closestPlayer.GetComponent<PlayerController>().dead == false)
                {
                    player = closestPlayer;
                    closestDistance = checkDistance;
                }

            }
        }
        if (player != null)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (!dead)
            {
                Attack();
            }
        }
    }
    private void FixedUpdate()
    {
        if (!dead && player != null)
        {
            Move();
        }
    }
}