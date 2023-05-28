using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    public CinematicManager _cinematicManager;
    public int triggerIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cinematicManager.PlayCinematic(triggerIndex);
        }
    }
}
