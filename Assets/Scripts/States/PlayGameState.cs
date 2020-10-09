using UnityEngine;

public class PlayGameState : MonoBehaviour, IState
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject scoreBar;

    public void Enter()
    {
        Debug.Log("Entered game state!");
        if(!player)
        {
            Debug.LogError("Player can't be null!");
        }
        if(!spawner)
        {
            Debug.LogError("Enemy Spawner can't be null");
        }
        player.SetActive(true);
        healthBar.SetActive(true);
        scoreBar.SetActive(true);
        spawner.GetComponent<EnemySpawner>().enabled = true;
    }

    public void Exit()
    {
        player.SetActive(false);
        player.transform.position = new Vector3(0, 0, 0);
        healthBar.SetActive(false);
        scoreBar.SetActive(false);
        spawner.GetComponent<EnemySpawner>().enabled = false;
    }
}
