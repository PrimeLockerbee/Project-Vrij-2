using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invector.vCharacterController.AI
{

    public class Terminal : MonoBehaviour
    {
        public KeyCode interactKey = KeyCode.E;
        public GameObject door;

        private bool playerInRange = false;
        private bool doorOpened = false;

        public CompanionController ccontroler;

        public GameObject popupText;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && ccontroler.isFollowing == true)
            {
                playerInRange = true;
                ShowPopup();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && ccontroler.isFollowing == true)
            {
                playerInRange = false;
                HidePopup();
            }
        }

        private void Update()
        {
            if (playerInRange && !doorOpened && Input.GetKeyDown(interactKey) && ccontroler.isFollowing == true)
            {
                OpenDoor();
            }
        }

        private void ShowPopup()
        {
            // Implement your on-screen popup logic here
            popupText.SetActive(true);
            Debug.Log("Press " + interactKey.ToString() + " to interact");
        }

        private void HidePopup()
        {
            // Implement your popup hide logic here
            popupText.SetActive(false);
            Debug.Log("Popup hidden");
        }

        private void OpenDoor()
        {
            // Implement your door opening logic here
            door.SetActive(false);
            doorOpened = true;
        }
    }
}