using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.ProBuilder.Shapes;

public class Rotation : MonoBehaviour
{
    public Transform map;
    public float rotationSpeed = 100f; // Tốc độ xoay

    private bool canRotate = false;
    private bool hasRotated = false;
    private float targetRotation = 0f; // Góc xoay đích
    private float currentRotation = 0f; // Góc xoay hiện tại

    private void Update()
    {
        ProBuilderMesh pbMesh = map.GetComponent<ProBuilderMesh>();
        pbMesh.CenterPivot(null);

        if (canRotate && !hasRotated && Input.GetKeyDown(KeyCode.E))
        {
            targetRotation -= 90f; // Cập nhật góc xoay đích
            hasRotated = true;
        }

        // Xoay dần đến góc xoay đích
        if (currentRotation != targetRotation)
        {
            float step = rotationSpeed * Time.deltaTime;
            currentRotation = Mathf.MoveTowards(currentRotation, targetRotation, step);
            map.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
            canRotate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Collider>().enabled = true;
            canRotate = false;
        }
    }
}
