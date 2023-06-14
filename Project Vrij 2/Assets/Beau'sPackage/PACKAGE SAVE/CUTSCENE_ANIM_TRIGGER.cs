using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class CUTSCENE_ANIM_TRIGGER : MonoBehaviour
{
    public PlayableDirector Cutscene1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("test");
            Cutscene1.Play();
            GetComponent<BoxCollider>().enabled = false;

        }

    }
}