using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PreGameState : MonoBehaviour, IState
{
    [SerializeField] GameObject waitScreen;
    [SerializeField] Text waitText;

    private bool isGameStarted;
    private Coroutine coroutine;

    public void Enter()
    {
        Debug.Log("Entered Pre Game State!");
        isGameStarted = false;
        coroutine = StartCoroutine(WaitForStart());
        waitScreen.SetActive(true);
    }

    public void Exit()
    {
        StopCoroutine(coroutine);
        waitScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.GetCurrentState() == this)
        {
            GameManager.Instance.SetGameState(StateType.PlayGameState);
        }
    }

    IEnumerator WaitForStart()
    {
        float time = 0;
        float colorValue = waitText.color.r;  // r = g = b = 50 (in this case)
        while (!isGameStarted)
        {
            time += Time.deltaTime;
            float value = Mathf.PingPong(time, 0.5f) + 0.5f;
            waitText.color = new Color(colorValue, colorValue, colorValue, value);
            yield return null;
        }
    }
}
