using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class HealingReset : MonoBehaviour
{
    public float healthAmount = 20f; // Amount of health to regenerate
    public float rotationSpeed = 60f; // Rotation speed of the healing object
    public Transform HealthRegen;
    public Transform respawnPoint;

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
                transform.position = respawnPoint.position;
            }
        }
    }
}
