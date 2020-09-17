using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardVolume : MonoBehaviour
{
    [Header("Hazard's Base Stats")]
    [SerializeField] float _moveSpeed = 3f;
    [SerializeField] float _stunTimer = 1;

    Rigidbody _rb = null;
    public Transform playerShip;

    private float _moveSpeedFinal;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _moveSpeedFinal = _moveSpeed;
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
        MoveEnemy();
    }

    void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerShip.position, _moveSpeedFinal * Time.deltaTime);
    }

    public void Stun()
    {
        _moveSpeedFinal = 0;
        DelayHelper.DelayAction(this, ResetMoveSpeed, _stunTimer);
    }

    void ResetMoveSpeed()
    {
        _moveSpeedFinal = _moveSpeed;
    }
}
