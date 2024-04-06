using UnityEngine;
using UnityEngine.ProBuilder;

public class Key : MonoBehaviour
{
    public Collider gate;
    public float rotationSpeed = 60f;
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gate.isTrigger = true;
            Destroy(gameObject); 
        }
    }
}
