using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ObjectiveCompass : MonoBehaviour
{
    public Transform indicatorTransform;
    public List<Objective> objectives;
    public TextMeshProUGUI objectiveText;
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
        Objective objective = objectives[index];
        objective.isActive = true;
        objective.gameObject.SetActive(true);
        objectiveText.gameObject.SetActive(true); // Activate the shared objective text
        objectiveText.text = objective.objectiveDescription; // Update the text
    }

    void SetObjectiveInactive(int index)
    {
        Objective objective = objectives[index];
        objective.isActive = false;
        objective.gameObject.SetActive(false);
        objectiveText.gameObject.SetActive(false); // Deactivate the shared objective text
    }
}
