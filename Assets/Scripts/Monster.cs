using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Vision vision;

    [SerializeField] private float speed = 1f;

    private void Start()
    {
        vision = GetComponentInChildren<Vision>();
    }

    private void Update()
    {
        if(vision.InRange)
        {
            var protection = vision.target.GetComponent<PlayerController>().ProtectionRadius;
            var distance = Vector3.Distance(transform.position, vision.target.transform.position);
            if (distance < protection)
            {
                transform.position += (transform.position - vision.target.transform.position).normalized * speed * 2 * Time.deltaTime;
                return;
            }
            else if (distance <= protection+1)
                return;

            transform.position = Vector3.MoveTowards(transform.position, vision.target.transform.position, speed * Time.deltaTime);
        }
    }
}
