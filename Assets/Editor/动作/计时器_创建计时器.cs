using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 计时器_创建计时器 : 动作
{
    public string 动作描述 = "创建计时器";
    public int 计时器类型;
    public float 计时周期;
    public List<动作> 执行动作list = new List<动作>();
    public string 计时器name;

    public override void 数据项目新增(string type)
    {
        switch (type)
        {
            case "判断动作":
                执行动作list.Add(new 判断动作());
                break;
            case "计时器_创建计时器":
                执行动作list.Add(new 计时器_创建计时器());
                break;
            case "技能_造成伤害":
                //执行动作list.Add(new 技能_造成伤害());
                break;
            case "技能_施加buff":
                //执行动作list.Add(new 技能_施加buff());
                break;
        }
    }

    public override void 渲染()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(动作描述, GUILayout.Width(100));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("计时器变量名"));
            计时器name = EditorGUILayout.TextField("计时器变量名" , 计时器name, GUILayout.MaxWidth(150));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("计时器类型"));
            计时器类型 = EditorGUILayout.Popup("计时器类型", 计时器类型, G.计时器类型_Array, GUILayout.MaxWidth(200));
            计时周期 = EditorGUILayout.FloatField("计时周期", 计时周期, GUILayout.MaxWidth(200));
            if (GUILayout.Button("新增 计时结束触发", GUILayout.MaxWidth(100)))
            {
                重复窗口.Popup(Vector2.zero).设置页面参数(this, "行为", "行为类别", "行为二级类别", G.行为类型Array);
            }
        }
        EditorGUILayout.EndHorizontal();
        渲染全部行为项目();
    }

    private void 渲染全部行为项目()
    {
        for (int i = 0; i < 执行动作list.Count; i++)
        {
            执行动作list[i]?.渲染();
        }
    }
}