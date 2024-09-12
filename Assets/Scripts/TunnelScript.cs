using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelScript : MonoBehaviour
{
    [SerializeField] PlayerBehaviour player;
    [SerializeField] float speed = 0;
    public float spawnDistance = 10f;
    public GameObject tunnel;
    private Transform initalPos;
    private bool hasChild = false;
    // Start is called before the first frame update
    void Start()
    {
        initalPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.right * 0.5f /** (player.speed * 0.1f)*/);

        Vector3 mPos = transform.position;
        if (mPos.x <= 80f && !hasChild)
        {
            hasChild = true;
            Vector3 spawnPos = initalPos.position;
            spawnPos.x = mPos.x + spawnDistance;
            Instantiate(tunnel, spawnPos, Quaternion.identity);

            if (mPos.x < 70f)
                Destroy(gameObject);
        }
    }
}
