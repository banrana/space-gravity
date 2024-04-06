using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class Healing : MonoBehaviour
{
    public float healthAmount = 20f;
    public float rotationSpeed = 60f;
    public Transform HealthRegen;

    private void Update()
    {
        ProBuilderMesh pbMesh = HealthRegen.GetComponent<ProBuilderMesh>();
        pbMesh.CenterPivot(null);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.IncreaseHealth(healthAmount);
                //PlayHealingEffect();
                Destroy(gameObject);
            }
        }
    }

    /*private void PlayHealingEffect()
    {
        if (HealthRegen != null)
        {
            Instantiate(HealthRegen, transform.position, Quaternion.identity);
        }
    }*/
}
