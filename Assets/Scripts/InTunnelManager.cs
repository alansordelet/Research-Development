using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTunnelManager : MonoBehaviour
{
    public static InTunnelManager instance { get; private set; }

    public Collider colliderSmallTunnel;
    public Collider colliderBigTunnel;
    public GameObject bigTunnel;
    public Transform player;
    public static InTunnelManager GetInstance()
    {
        return instance;
    }
    public bool inTunnel = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colliderBigTunnel != null && colliderSmallTunnel != null)
        {
            if (colliderSmallTunnel.GetComponent<Collider>().bounds.Contains(player.position))
            {
                inTunnel = true;
                bigTunnel.SetActive(false);
            }
            else if (colliderBigTunnel.GetComponent<Collider>().bounds.Contains(player.position))
            {
                inTunnel = true;
            }
            else
            {
                inTunnel = false;
                bigTunnel.SetActive(true);
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           inTunnel = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inTunnel = false;
        }
    }
}
