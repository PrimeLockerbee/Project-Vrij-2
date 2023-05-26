using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ENTIRY SCRIPT IS TEMPORARY PLS ERASE IF NOT NEEDED ANYMORE MEEEEEEEP
public class EndingManager : MonoBehaviour
{
    [SerializeField] GameObject _enemy01;
    [SerializeField] GameObject _enemy02;
    [SerializeField] GameObject _enemy03;
    [SerializeField] GameObject _enemy04;

    [SerializeField] GameObject _endingPanel;

    [SerializeField] GameObject _playerUI;
    [SerializeField] GameObject _aimCanvas;
    //[SerializeField] GameObject _Player;

    //Checks if all the enemies of the last encounter are dead, if so upon entering the trigger activates the ending screen.
    private void OnTriggerEnter(Collider other)
    {
        if(_enemy01 == null && _enemy02 == null && _enemy03 == null && _enemy04 == null)
        {
            Debug.Log("WORKS");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _playerUI.SetActive(false);
            _endingPanel.SetActive(true);
        }
    }
}
