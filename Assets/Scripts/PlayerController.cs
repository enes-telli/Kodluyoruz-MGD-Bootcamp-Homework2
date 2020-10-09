using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float moveSpeed = 0.5f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int damage = 20;
    [SerializeField] [Range(0f, 0.8f)] float padding = 0.5f;
    [SerializeField] Transform gun;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float firingPeriod = 0.5f;

    [SerializeField] HealthBar healthBar;
    [SerializeField] ScoreBar scoreBar;

    private float xMin, xMax, yMin, yMax;
    private int currentHealth;
    private Vector3 lookDirection;
    private float lookAngle;
    private GameObject bullet;

    private void Start()
    {
        SetTheMoveBoundaries();
        currentHealth = maxHealth;
        healthBar.UpdateSliderValue(maxHealth, maxHealth);
    }

    void Update()
    {
        Move();
        Rotate();
        Fire();
    }

    private void Move()
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed;
        float yMovement = Input.GetAxis("Vertical") * moveSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + xMovement, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + yMovement, yMin, yMax);
        transform.position = new Vector3(newXPos, newYPos, 0);
    }

    private void Rotate()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90);
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FireContinuously());
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            bullet = Instantiate(bulletPrefab, gun.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = gun.up * bulletSpeed;
            yield return new WaitForSeconds(firingPeriod);
        }
    }

    public void HitDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateSliderValue(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            currentHealth = maxHealth;
            healthBar.UpdateSliderValue(maxHealth, maxHealth);
            scoreBar.ResetScoreValue();
            GameManager.Instance.SetGameState(StateType.PreGameState);
        }
    }

    private void SetTheMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public int GetDamage()
    {
        return damage;
    }
}
