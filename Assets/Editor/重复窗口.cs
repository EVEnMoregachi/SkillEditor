using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class 重复窗口 : EditorWindowBase
{
    public static 重复窗口 Popup(Vector3 positon)
    {
        重复窗口 window = GetWindowWithRectPrivate(
            typeof(重复窗口),
            new Rect(new Vector2(0, 0), new Vector2(300, 200)),
            true,
            "弹窗") as 重复窗口;

        // 优先级焦点管理器
        EditorWindowManager.add重复窗口(window);

        // 窗口偏移
        int offset = 20;
        window.position = new Rect(new Vector2(positon.x + offset, positon.y + offset), new Vector2(300, 200));

        window.Show();
        window.Focus();
        return window;
    }
    // unity的弹窗只能创建一个，自己重写逻辑可以创建多个
    private static EditorWindow GetWindowWithRectPrivate(Type t, Rect rect , bool utility , string title)
    {
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
        EditorGUILayout.LabelField("设置", GUILayout.Width(100));
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("测试窗口", GUILayout.Width(100)))
        {
            Popup(this.position.position);
        }

        if (GUILayout.Button("取消", GUILayout.Width(100)))
        {
            this.Close();
        }

        if (GUILayout.Button("聚焦", GUILayout.Width(100)))
        {
            EditorWindowManager.focusWindow();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.TextArea("优先级：" + 优先级);
        GUILayout.EndVertical();
    }
    private void OnFocus()
    {
        // 始终聚焦最高优先级的弹窗
        EditorWindowManager.focusWindow();
    }

    private void OnDestroy()
    {
        EditorWindowManager.Remove重复窗口(this);
        EditorWindowManager.focusWindow();
    }
}
