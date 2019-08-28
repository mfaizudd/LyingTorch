using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Torch : MonoBehaviour
{
    [SerializeField] Light2D _light;
    public float speed = 1f;
    public float protectionRadius;
    public bool infinity = false;
    public float outerRadiusMin = 6, outerRadiusMax = 7;
    public CircleCollider2D protection;

    private void Start()
    {
        if(_light == null)
            _light = GetComponent<Light2D>();
        if (protection == null)
            protection = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        _light.pointLightOuterRadius = Random.Range(outerRadiusMin, outerRadiusMax);

        if (_light.intensity <= 0) return;
        if (infinity) return;

        _light.intensity -= (speed / 100) * Time.deltaTime;
        protectionRadius = _light.pointLightOuterRadius * _light.intensity;
        if(protection!=null)
            protection.radius = protectionRadius;
    }

    public void Pick(Torch actor)
    {
        actor._light.intensity = 1f;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, protectionRadius);
    }
}
