using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyReset : MonoBehaviour
{
    public Collider gate;
    public float rotationSpeed = 60f;
    public Transform respawnPoint;
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gate.isTrigger = true;
            transform.position = respawnPoint.position;
        }
    }
}
