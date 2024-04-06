using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailInfo : MonoBehaviour
{
    //Thông tin damage
    public CombatController combatController;
    public TextMeshProUGUI damageText;
    //Thông tin CoolDown Dash
    public Controller controller;
    public TextMeshProUGUI dashCooldownText;
    void Start()
    {
        combatController = FindObjectOfType<CombatController>();
        damageText.text = "DMG: " + combatController.attackDamage.ToString();
    }

    void Update()
    {
        if (combatController.attackDamage != float.Parse(damageText.text.Substring(5)))
        {
            damageText.text = "DMG: " + combatController.attackDamage.ToString();
        }
        // Cập nhật thời gian đếm ngược Dash
        if (controller.dashTimer > 0f)
        {
            dashCooldownText.text = "DASH: " + controller.dashTimer.ToString("F1") + "s";
        }
        else
        {
            dashCooldownText.text = "DASH: READY";
        }
    }
}