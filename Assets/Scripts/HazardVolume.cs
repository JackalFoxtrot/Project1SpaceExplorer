using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();

        if (playerShip != null)
        {
            playerShip.Kill();
            StartCoroutine(RestartGame(playerShip));
        }
    }

    IEnumerator RestartGame(PlayerShip playerShip)
    {
        yield return new WaitForSeconds(playerShip.getDeathTimer());

        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

}
