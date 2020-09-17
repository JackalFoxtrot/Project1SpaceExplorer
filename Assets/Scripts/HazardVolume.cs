using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardVolume : MonoBehaviour
{
    [Header("Hazard's Base Stats")]
    [SerializeField] float _moveSpeed = 3f;
    [SerializeField] float _stunTimer = 5;

    Rigidbody _rb = null;
    public Transform playerShip;
    private bool stunned = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();

        if (playerShip != null)
        {
            playerShip.Kill();
            DelayHelper.DelayAction(this, RestartGame, playerShip.getDeathTimer());
        }
    }

    void RestartGame()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    private void FixedUpdate()
    {
        if (this.isActiveAndEnabled)
        {
            MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        if(stunned)
        {
            StartCoroutine(StunDuration());
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerShip.position, _moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator StunDuration()
    {
        yield return new WaitForSeconds(_stunTimer);
        stunned = false;
    }

    public void setStun(bool stunValue)
    {
        stunned = stunValue;
    }

}
