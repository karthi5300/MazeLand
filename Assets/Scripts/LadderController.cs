using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    private bool isLadderNotActivated = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isLadderNotActivated)                           
        {
                isLadderNotActivated = false;               //setting to activate ladder only once
                gameObject.transform.Rotate(0, 0, 90);      //rotate the ladder in z axis when player interacts with it
        }
    }
}
