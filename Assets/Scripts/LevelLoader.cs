using System;
using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    public void LoadUI()
    {
        StartCoroutine(CoLoadUI());
    }

    public void LoadMain()
    {
        Application.LoadLevel("Main");
    }

    IEnumerator CoLoadUI()
    {
        Debug.Log("Loading UI " + DateTime.Now);
        AsyncOperation async = Application.LoadLevelAdditiveAsync("UI");
        yield return async;
        Debug.Log("Loaded UI " + DateTime.Now);
    }

}
