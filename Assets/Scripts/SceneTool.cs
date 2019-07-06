using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTool : SingletonBase<SceneTool> {
    private const LoadSceneMode LOADSCENE_MODE = LoadSceneMode.Single;
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <param name="mode">模式</param>
    /// <param name="async">异步加载</param>
    public void LoadScene(string sceneName,
        LoadSceneMode mode= LOADSCENE_MODE, bool async = true)
    {
        if (async) {
            StartCoroutine(LoadSceneAsync(sceneName, mode));
        }
        else
            SceneManager.LoadScene(sceneName,mode);
    }
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneBuildIndex">场景名</param>
    /// <param name="mode">模式</param>
    /// <param name="fade">是否淡入淡出</param>
    public void LoadScene(int sceneBuildIndex, 
        LoadSceneMode mode = LOADSCENE_MODE, bool async = true)
    {
        if (async)
        {
            StartCoroutine(LoadSceneAsync(sceneBuildIndex, mode));
        }
        else
            SceneManager.LoadScene(sceneBuildIndex, mode);

    }
    
    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSceneAsync(string sceneName,
        LoadSceneMode mode)
    {
        AsyncOperation asy=SceneManager.LoadSceneAsync(
            sceneName, mode);

        while (!asy.isDone)
        {
            yield return null;
        }
    }
    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSceneAsync(int sceneBuildIndex,
        LoadSceneMode mode)
    {
        AsyncOperation asy=SceneManager.LoadSceneAsync(
            sceneBuildIndex, mode);
        //防止加载完跳出：
        asy.allowSceneActivation = false;

        while (asy.progress < 0.9f)
        {
            //Debug.Log("fadeIsDone:" + fadeIsDone+ "isDone:"+ asy.isDone);
            //Debug.Log("progress:"+asy.progress);
            yield return null;
        }
        ///加载完成且淡出特效消失后跳出：
        //Debug.Log("fadeIsDone:" + fadeIsDone + "isDone:" + asy.isDone);
        asy.allowSceneActivation = true;
    }
    
}
