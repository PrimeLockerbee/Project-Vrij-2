using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivater : MonoBehaviour
{
    public void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
