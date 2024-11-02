//using System.Numerics;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class enemyPatrol : MonoBehaviour
{

    public Rigidbody rb;
    NavMeshAgent agent;

    public float range = 10f;
    public float speed = 5f;
    public Vector3 home;

    private Vector3 previous;
    private Vector3 current;
    public Vector3 check;

    public int stuck = 0;

    public bool facingRight = true;
    public bool objectStraight = false;
    public bool jumpNow = false;

    public Vector3 testRay;

    private Vector3 velocity; // To hold our current velocity
    private Vector2 move; // To hold our move

    public float moveSpeed = 6f; // To calibrate our movement
    public float jumpHeight = 5; // To calibrate our jump
    public float gravity = -9.81f; // To calibrate our gravity

    public Transform ground; // Ground check empty goes in here
    public float distanceToGround = 0.4f; // How close the ground needs to be to register
    public LayerMask groundMask; // What layer is ground?

    public bool doJump = false;
    public bool checkDown = false;
    public bool checkForward = false;
    public bool checkUp = false;

    public bool gaming;
    // public static bool Raycast(agent.transform.position, testRay, out hit, 5f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.stoppingDistance = 2;

        current = agent.transform.position;

        Vector3 previous = current;


        //  gameObject.transform.position = home;

        home = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z);

        Vector3 check = new Vector3(range, 0, 0);

        agent.SetDestination(home + new Vector3(range, 0, 0));

        // agent.SetDestination(home + new Vector3(range, 0, 0));



    }




    // Update is called once per frame
    void Update()
    {
       
        Jump();
        Patrol();
        moveBack();
        
            if (facingRight)
            {
                testRay = new Vector3(1, -.5f, 00);
            }
            else testRay = new Vector3(-1, -.5f, 0);


        //Quaternion.Euler(0, 0, -30) * agent.transform.forward
        RaycastHit hitDown;
        if (Physics.Raycast(agent.transform.position, testRay, out hitDown, 10f))
        {

            Debug.Log(hitDown.transform.name + " is down");
            checkDown = true;
        }

        RaycastHit hitForward;

        if (Physics.Raycast(agent.transform.position, agent.transform.forward, out hitForward, 7f))
        {
            Debug.Log(hitForward.transform.name + " is forward");

            checkForward = true;
        }

        RaycastHit hitUp;
        if (!Physics.Raycast(agent.transform.position, new Vector3(agent.transform.forward.x, .5f, 0), out hitUp, 6f))
        {
            Debug.Log(hitUp.transform.name + " is up");

            checkUp = true;
        }

        else
        {
            checkDown = checkForward = checkUp = false;
        }
        // SpherecastCommand

        if (checkDown == checkUp == true && checkForward == false ) jumpNow = true;
        if (checkForward == true && checkUp == false) jumpNow = true;


    }


    void Patrol()
    {


        if (agent.transform.position.x >= range - 0.5 + home.x)
        {
            agent.SetDestination(home - new Vector3(range, 0, 0));
            facingRight = false;
        }

        else if (agent.transform.position.x <= home.x - range + .5)
        {
            agent.SetDestination(home + new Vector3(range, 0, 0));
            facingRight = true;
        }
    }



    void moveBack()
    {
        current = agent.transform.position;

        if (previous == current) stuck++;

        else if (previous != current) stuck = 0;

        if (stuck == 200)
        {

            if (agent.transform.position.x <= home.x)
            {
                agent.SetDestination(home + new Vector3(range, 0, 0));
                facingRight = true;
            }

            else if (agent.transform.position.x > home.x)
            {
                agent.SetDestination(home - new Vector3(range, 0, 0));
                facingRight = false;
            }

            stuck = 0;
        }

        previous = current;

    }

    private void Jump()
    {

        if (jumpNow == true && isGrounded())
        {
            GetComponent<NavMeshAgent>().enabled = false;

            rb.AddForce(new Vector3(transform.forward.x, .5f, transform.forward.z).normalized * 150, ForceMode.Impulse);

            StartCoroutine(Wait());
        }





    }
    IEnumerator Wait()
    {

        yield return new WaitForSeconds(.8f);
        GetComponent<NavMeshAgent>().enabled = true;
        jumpNow = false;
    }


    private bool isGrounded()
    {
        return Physics.CheckSphere(ground.position, distanceToGround, groundMask);
    }


}