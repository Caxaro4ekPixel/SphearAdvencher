using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadSceneDeth : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Text timerText;
    [SerializeField] private int second = 5;
    [SerializeField] private int tempSeconds;

    private void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        tempSeconds = second;
    }

    IEnumerator DoCheck()
    {
        for (int i = 0; i < second; i++)
        {
            timerText.text = tempSeconds.ToString();
            tempSeconds -= 1;
            yield return new WaitForSeconds(1);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SphereCollider>().enabled = false;
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            deathPanel.SetActive(true);
            StartCoroutine("DoCheck");
        }
    }
}
