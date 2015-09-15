using UnityEngine;
using System.Collections;

public class KeepMeAround : MonoBehaviour {
    public void KeepAround()
    {
        var canvas = GameObject.FindGameObjectWithTag("UI");
        if (canvas == null)
        {
           Debug.LogWarning("Couldn't find UI canvas");
        }
        DontDestroyOnLoad(canvas);
    }

}
