using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinVolume : MonoBehaviour
{
    public GameObject uiController;
    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();

        if (playerShip != null)
        {
            playerShip.Win();
            uiController.SetActive(true);
        }
    }
}
