using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCompass : MonoBehaviour
{
    public Transform indicatorTransform;
    public List<Objective> objectives;
    private int currentObjectiveIndex = 0;

    void Start()
    {
        SetObjectiveActive(currentObjectiveIndex);
    }

    void Update()
    {
        if (objectives.Count == 0 || indicatorTransform == null || currentObjectiveIndex >= objectives.Count)
        {
            return;
        }

        Vector3 direction = objectives[currentObjectiveIndex].transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        indicatorTransform.rotation = rotation;
    }

    public void CompleteObjective(Objective completedObjective)
    {
        int index = objectives.IndexOf(completedObjective);

        if (index == currentObjectiveIndex)
        {
            SetObjectiveInactive(currentObjectiveIndex);

            currentObjectiveIndex++;
            if (currentObjectiveIndex >= objectives.Count)
            {
                // All objectives completed
                Debug.Log("All objectives completed!");
                return;
            }

            SetObjectiveActive(currentObjectiveIndex);
        }
    }

    void SetObjectiveActive(int index)
    {
        objectives[index].isActive = true;
        objectives[index].gameObject.SetActive(true);
    }

    void SetObjectiveInactive(int index)
    {
        objectives[index].isActive = false;
        objectives[index].gameObject.SetActive(false);
    }
}

