using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelServices
{

    public class LadderController : MonoBehaviour
    {
        [SerializeField] private GameObject ladder;

        private bool isLadderNotActivated = true;

        private void OnTriggerEnter(Collider other)
        {
            if (isLadderNotActivated)
            {
                isLadderNotActivated = false;               //setting to activate ladder only once
                ladder.GetComponent<BoxCollider>().enabled = false;
                ladder.transform.Rotate(0, 0, 90);      //rotate the ladder in z axis when player interacts with it
            }
        }
    }
}