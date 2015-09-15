using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class FinishCompiling : EditorWindow
{
    [SerializeField]
    private static Texture2D _compilationDone;
    [SerializeField]
    private static Texture2D _compilationInProgress;

    [SerializeField] private bool _wasCompiling=false;


    // Update is called once per frame
    void Update()
    {
       // Debug.Log(DateTime.Now);
        if (EditorApplication.isCompiling)
        {
            Repaint();
            //EditorApplication.isPlaying = false;
        }
        else if( _wasCompiling)
        {
            //If we get to an update and this flag is set, we'll assume OnGui isn't called yet
            //which only gets called on changes, so we'll have to force a redraw
           _wasCompiling = false;
            Debug.Log("Resetting wasCompiling, btw _startCompiling is null?" + (_startedCompiling==null));
            Repaint();
        }
    }


    [MenuItem("Compilation Debug/Watch")]
    static void Init()
    {
        FinishCompiling window = (FinishCompiling)
            GetWindowWithRect(typeof(FinishCompiling), new Rect(0, 0, 600, 140));
        _compilationDone = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        _compilationDone.SetPixel(0, 0, new Color(0f, 1f, 0f));
        _compilationDone.Apply();

        _compilationInProgress = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        _compilationInProgress.SetPixel(0, 0, new Color(1f, 0f, 0f));
        _compilationInProgress.Apply();

        window.Show();

    }
    //arg DateTime is a struct, avoid serializing structs. Store as string
    [SerializeField]
    private string _startedCompiling;
    [SerializeField]
    private string _endCompiling;
    [SerializeField]
    private string _status = "No compilation yet";
    [SerializeField]
    private string _elapsed = "Elapsed: N/A";
    void OnGUI()
    {
        if (string.IsNullOrEmpty(_startedCompiling))
        {
            Debug.Log("_startedCompiling is null");
        }
        //Cant have in init, as the static data gets cleared and that doesn't get raised if the window is still open.
        if (_compilationDone == null)
        {
            // Debug.Log("_compilationDone is null, regenerating");
            _compilationDone = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            _compilationDone.SetPixel(0, 0, new Color(0f, 1f, 0f));
            _compilationDone.Apply();
        }

        if (_compilationInProgress == null)
        {
            //   Debug.Log("_compilationInProgress is null, regenerating");

            _compilationInProgress = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            _compilationInProgress.SetPixel(0, 0, new Color(1f, 0f, 0f));
            _compilationInProgress.Apply();
        }

        //Debug.Log(System.DateTime.Now.Ticks);
        if (EditorApplication.isCompiling)
        {
            

            _wasCompiling = true;
            if (string.IsNullOrEmpty(_startedCompiling))
            {
                _startedCompiling = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
                _status = "Compiling Started1 " + _startedCompiling;
            }
            else
            {
                //still compiling, hang tight with last statuss
            }
            //Draw red
            GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), _compilationInProgress, ScaleMode.StretchToFill);
        }
        else
        {
            //We aren't compiling anymore
            GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), _compilationDone, ScaleMode.StretchToFill);
            //Debug.Log("All done, _started == minvalue?" + (_startedCompiling == DateTime.MinValue).ToString());
            if (!string.IsNullOrEmpty(_startedCompiling))
            {
                //were we compiling and need to update text that we're done?
                _endCompiling = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");

                //
                //
                //
                _status = string.Format("Compiling Started2 {0} Ended {1}", _startedCompiling, _endCompiling);
                //Reset to default
                DateTime endDate = DateTime.ParseExact(_endCompiling, "MM/dd/yyyy hh:mm:ss.fff tt", System.Globalization.CultureInfo.InvariantCulture);
                DateTime startDate = DateTime.ParseExact(_startedCompiling, "MM/dd/yyyy hh:mm:ss.fff tt", System.Globalization.CultureInfo.InvariantCulture);
                _elapsed ="Compilation Time:" + (endDate - startDate).TotalSeconds + " seconds";
                _startedCompiling = null;

            }

        }
        EditorGUILayout.LabelField(_status);
        EditorGUILayout.LabelField(_elapsed);

        //EditorGUILayout.TextField(EditorApplication.isCompiling ? "Yes" : "No");

        //EditorGUILayout.LabelField("Compiling:", EditorApplication.isCompiling ? "YES" : "No");
    }
}
