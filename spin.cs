using UnityEngine;

public class spin : MonoBehaviour
{
    private float a;
    private float b;   
    public Vector3 pos;
   
    void Start()
    {
        
        a = 0;
        b = 0;

        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, .7f, 0);
      
        a += .005f;

        b = .5f * Mathf.Sin(a)+1;
        transform.position = new Vector3(pos.x, b, pos.z);
    }
}

