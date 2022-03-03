using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public bool showFPS;
    float deltaTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    public void showFPSToggle()
    {
        showFPS = !showFPS;
        Debug.Log("show FPS: " + showFPS);
    }
    private void OnGUI()
    {
        if (showFPS)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.UpperLeft;
            style.fontStyle = (FontStyle)(h * 2 / 100);
            style.normal.textColor = Color.red;

            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(new Rect(0, 0, w, h * 2 / 100), text, style);
        }
    }
}
