using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenControl : MonoBehaviour
{
    public GameObject loadingScreenObj;
    public Slider slider;

    private AsyncOperation async;

    public void LoadScreen(int scene)
    {
        StartCoroutine(LoadingScreen(scene));
    }

    private IEnumerator LoadingScreen(int scene)
    {
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}