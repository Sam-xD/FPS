using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using StarterAssets;



public class Shooting : MonoBehaviour
{

    private StarterAssetsInputs s;
    public GameObject bullet;
    [SerializeField]
    private GameObject inst;
    GameObject g;
    public Text t;
    private Queue<GameObject> ob;
    //int n = 0;
    Ray ray;


    IEnumerator Shoot(GameObject a)
    {
        a.SetActive(true);
        a.transform.position = inst.transform.position;
        a.transform.rotation = transform.rotation;
        //Debug.Log(inst.transform.forward);
        //Debug.Log(inst.transform.rotation);
        a.GetComponent<Rigidbody>().AddForce(transform.forward * 4000f);
        
        yield return new WaitForSeconds(1f);
        if (!ob.Contains(a))
        {
            a.SetActive(false);
            ob.Enqueue(a);
            //Debug.Log("dq");
        }
        yield return null;
    }
    void Start()
    {

        ob = new Queue<GameObject>();
        //ray = new Ray(inst.transform.position, transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction*500,Color.red);
        
        
        s = transform.root.GetComponent<StarterAssetsInputs>();

        for (int i = 0; i < 10; i++)
        {
            g = Instantiate(bullet);
            g.SetActive(false);
            ob.Enqueue(g);
        }


    }
    
    void FixedUpdate()
    {

    }
    void Update()
    {
        if (s.Shoot)
        {
            //Debug.Log("logic works");
            g = ob?.Dequeue();
            StartCoroutine(Shoot(g));
            s.Shoot = false;
        }
        RaycastHit r;
        if (Physics.Raycast(inst.transform.position,inst.transform.forward, out r))
        {
            t.text = ""+r.distance;
        }
    }

}


