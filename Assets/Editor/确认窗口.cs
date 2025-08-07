using System;
using UnityEditor;
using UnityEngine;


public class 确认窗口 : EditorWindowBase
{
    static Vector2 分辨率 = new Vector2(300, 120);

    public static 确认窗口 Popup(Vector4 position)
    {
        确认窗口 window = GetWindowWithRectPrivate(typeof(确认窗口), new Rect(new Vector2(0,0), 分辨率), true, "提示") as 确认窗口;
        window.Show();
        window.Focus();
        return window;
    }

    private static EditorWindow GetWindowWithRectPrivate(Type t, Rect rect, bool utility, string title)    {
        EditorWindow editiorWindow = null;
        editiorWindow = (CreateInstance(t) as EditorWindow);
        editiorWindow.minSize = new Vector2(rect.width, rect.height);
        editiorWindow.maxSize = new Vector2(rect.width, rect.height);
        editiorWindow.position = rect;
        if (title != null)
        {
            editiorWindow.titleContent = new GUIContent(title);
        }
        if (utility)
        {
            editiorWindow.ShowUtility();
        }
        else
        {
            editiorWindow.Show();
        }

        return editiorWindow;
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        {
            GUILayout.Space(12);
            GUILayout.Space(12);
            GUILayout.Space(12);
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("保存成功!");
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(12);
            GUILayout.Space(12);
            GUILayout.Space(12);
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("确定", GUILayout.Width(70)))
                {
                    this.Close();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private void OnFocus()
    {
        // 始终聚焦最高优先级的弹窗
        EditorWindowManager.focusWindow();
    }

    private void OnDestroy()
    {
        EditorWindowManager.focusWindow();
    }
}
