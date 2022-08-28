using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SandController : MonoBehaviour
{
    [SerializeField] private GameObject m_wall;

    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);        //Destroy the tile after player stepped out of it
        m_wall.SetActive(true);     //enabling the wall to stop player moving back        
    }

}
