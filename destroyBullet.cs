using System.Collections;
using UnityEngine;

public class destroyBullet : MonoBehaviour
{
    public float lifetime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag != "enemy" && other.tag != "enemyBullet")

            Destroy(gameObject);
            
            
         //   CompareTag("ignoreCollision")) 
      //  Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.FindGameObjectWithTag("ignoreCollision").GetComponent<Collider>());
        
            

        //transform.GetComponent<rangeAttack>()
        // gameObject.Equals("enemy")
        //&& !other.gameObject.Equals("enemyBullet(Clone)")

    }




    IEnumerator die()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

}
