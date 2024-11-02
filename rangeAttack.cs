using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class rangeAttack : MonoBehaviour
{
    public Transform player;
    public int health = 5;
    //bool hit = false;
    public int range = 20;
    public int damage = 1;
    private bool attacking = false;
    NavMeshAgent agent;
    public Rigidbody prefab; // The Prefab we want to spawn
    public GameObject enemybullet;
    public Vector3 offset;
    public int shootSpeed = 5;
    public int attackMode;
    public int shortAttackRange = 5;
    public float prevSpeed;

    void OnTriggerEnter(Collider other)
    {

        if (other.transform.root.CompareTag("Player")) GameManager.instance.changePlayerHealth(-damage);

    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        health = 5;
        float prevSpeed = agent.speed;
    }


    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {

            Vector3 Offset = transform.position - player.transform.position;
            float Distance = Offset.magnitude;

            if (Distance <= range && player.transform.position.y + 1 >= transform.position.y)
            {
                if(attackMode == 1)
                StartCoroutine(shootAttack());

                if(attackMode == 2) StartCoroutine(smallAttack());


                attacking = true;

            }
        }
    }

    IEnumerator smallAttack()
    {
        float prevSpeed = agent.speed;
        agent.speed = 0;
        for (int i = 0; i < 3; i++)
        {
            //Rigidbody clone = Instantiate(prefab, transform.position, Quaternion.identity);

            Debug.Log("doing attck" + i);
            if (offset.magnitude <= shortAttackRange)
            {
                GameManager.instance.changePlayerHealth(-damage);

            }
            yield return new WaitForSeconds(.5f);
        }

        yield return new WaitForSeconds(3.5f);
        agent.speed = prevSpeed;
        Debug.Log("dun");
        attacking = false;

    }


    IEnumerator shootAttack()
    {
        float prevSpeed = agent.speed;
        agent.speed = 0;
        for (int i = 0; i < 3; i++)
        {
            //Rigidbody clone = Instantiate(prefab, transform.position, Quaternion.identity);

            Debug.Log("doing attck" + i);
            shoot();
            yield return new WaitForSeconds(.8f);
        }


        yield return new WaitForSeconds(3.5f);
        agent.speed = prevSpeed;
        Debug.Log("dun");
        attacking = false;

    }

    void shoot()
    {
        Debug.Log("loading ammo");
        GameObject newBullet = Instantiate(enemybullet,new Vector3(transform.position.x, transform.position.y+.5f, transform.position.z), Quaternion.identity);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();

        Vector3 playerVector = (player.transform.position - transform.position).normalized;

        Vector3 direction = Quaternion.Euler(0, 0, Random.Range(-15, 45)) * playerVector;

        rb.AddForce(direction * shootSpeed, ForceMode.Impulse);

        attacking = false;
    }


}