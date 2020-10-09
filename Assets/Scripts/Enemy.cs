using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int damage = 20;
    [SerializeField] int health = 100;
    [SerializeField] float minSpeed = 2f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] int score = 3;

    private float randomSpeed;
    private PlayerController player;
    private ScoreBar scoreBar;

    private void Start()
    {
        scoreBar = FindObjectOfType<ScoreBar>();
        randomSpeed = Random.Range(minSpeed, maxSpeed);
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().HitDamage(damage);
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            HitDamage(player.GetDamage());
        }
    }

    public void MoveTowardsThePlayer(GameObject player)
    {
        StartCoroutine(StartTheMovement(player));
    }

    IEnumerator StartTheMovement(GameObject player)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, randomSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void HitDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
            scoreBar.UpdateScoreValue(score);
        }
    }
}
