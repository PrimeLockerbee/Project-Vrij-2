using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCutsceneChecker : MonoBehaviour
{
    [SerializeField] GameObject _enemy01;
    [SerializeField] GameObject _enemy02;
    [SerializeField] GameObject _enemy03;
    [SerializeField] GameObject _enemy04;

    [SerializeField] GameObject _CutsceneTrigger;

    //Checks if all the enemies of the encounter are dead, if so upon enemy death activates the cutscene.
    public void Update()
    {
        if (_enemy01 == null && _enemy02 == null && _enemy03 == null && _enemy04 == null)
        {
            _CutsceneTrigger.SetActive(true);
        }
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
