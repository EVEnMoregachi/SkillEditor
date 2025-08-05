using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class 重复窗口 : EditorWindowBase
{
    int 条件1级, 条件2级;
    public static string[] 条件1级Array, 条件2级Array;
    public static string 条件1级label, 条件2级label;
    static string 当前选择name;
    I动作 I动作_;



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
        OnEditorGUI();
    }

    void OnEditorGUI()
    {
        GUILayout.Space(12);
        GUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("设置", GUILayout.Width(600));
            EditorGUILayout.BeginHorizontal();
            {
                渲染条件页面();
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(100);
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("打开窗口", GUILayout.Width(100)))
                {
                    Popup(this.position.position);
                }
                if (GUILayout.Button("取消", GUILayout.Width(100)))
                {
                    this.Close();
                }
                if (GUILayout.Button("确定", GUILayout.Width(100)))
                {
                    I动作_.数据项目新增(EG.根据key获取动作类(new Vector2(条件1级, 条件2级)));
                    this.Close();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private void 渲染条件页面()
    {
        EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent(条件1级label));
        条件1级 = EditorGUILayout.Popup(条件1级label, 条件1级, 条件1级Array, GUILayout.MaxWidth(150));
        EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent(条件2级label));
        条件2级 = EditorGUILayout.Popup(条件2级label, 条件2级, G.根据ID选择二级菜单[当前选择name][条件1级], GUILayout.MaxWidth(150));
        switch (条件1级 + "^" + 条件2级)
        {
            case "3^0":// 创建计时器
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("创建计时器", GUILayout.Width(25));
                }
                EditorGUILayout.EndHorizontal();
                break;
        
        }
    }

    public void 设置页面参数(I动作 I动作, string name, string label1, string label2, string[] label1Array)
    {
        当前选择name = name;
        条件1级Array = label1Array;
        条件1级label = label1;
        条件2级label = label2;
        I动作_ = I动作;
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
