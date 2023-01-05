using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int livePoints;

    private GameObject currentProyectile;
    public GameObject proyectilePrefav;

    private Transform player;

    private Transform objective;
    private NavMeshAgent navMesh;
    private float time;
    public float timePerShot = 5;
    private bool shooting;

    [Header("animation")]
    public Animator enemyAnim;

    [Header("Audio")]
    public AudioClip[] audios;
    private AudioSource controlAudio;

    private void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
        enemyAnim = GetComponent<Animator>();
        enemyAnim.enabled = false;
        enemyAnim.enabled = true;
        player = GameObject.Find("Player").transform.GetChild(0).GetChild(0);
        objective = GameObject.Find("EnemyDestination").transform;
        navMesh = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        enemyAnim.enabled = true;
        livePoints = 1;
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.speed = 1;
        navMesh.destination = objective.position;
        if(shooting)
        {

            enemyAnim.SetBool("Shooting", true);
            navMesh.speed = 0;
        }
        time += Time.deltaTime;
        if(time > timePerShot)
        {
            shooting = true;
            time = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "arrow")
        {
            EnemyDamage();

        }
        if (other.tag == "sword")
        {
            EnemyDamage();
        }
        if (other.tag == "Finish")
        {
            Player.instance.DamageTaken();
            Destroy(gameObject);
        }
    }
    private void EnemyDamage() {
        livePoints -= 1;
        if(livePoints == 0)
        {
            Points.instance.AddPoints(5);
            Destroy(gameObject);
        }
    }
    public void NewProyectile()
    {
        controlAudio.PlayOneShot(audios[0], 1);
        currentProyectile = Instantiate(proyectilePrefav);
        currentProyectile.transform.parent = gameObject.transform;
        currentProyectile.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        currentProyectile.GetComponent<Rigidbody>().AddForce((player.transform.position - gameObject.transform.position) * 20.0f);
        currentProyectile.transform.parent = null;
    }
    public void StopAttack()
    {
        shooting = false;
        enemyAnim.SetBool("Shooting", false);
    }
}
