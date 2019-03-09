using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour
{

    void Start()
    {
        var Planet = GameObject.Find("Planet");
        Destroy(Planet.gameObject);
        StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        yield return new WaitForSecondsRealtime(15);
        SceneManager.LoadSceneAsync(PlayerPrefs.GetString("SceneLoad"));
    }

}
