using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Update()
    {
        
    }

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
