using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    public Material Material;
    public Material Material_2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Material"))
        {
            Renderer weaponRenderer = GetComponentInChildren<Renderer>();

            if (weaponRenderer != null)
            {
                weaponRenderer.material = Material;
            }
        }

        if (other.CompareTag("Material_2"))
        {
            Renderer weaponRenderer = GetComponentInChildren<Renderer>();

            if (weaponRenderer != null)
            {
                weaponRenderer.material = Material_2;
            }
        }
    }
}
