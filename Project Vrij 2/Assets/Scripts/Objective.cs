using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objective : MonoBehaviour
{
    public ObjectiveCompass compass;
    public bool isActive = false;

    public string objectiveDescription;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            compass.CompleteObjective(this);
        }
    }
}