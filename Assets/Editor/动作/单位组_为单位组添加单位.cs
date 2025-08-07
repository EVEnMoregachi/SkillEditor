using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 单位组_为单位组添加单位 : 动作
{
    public string 动作描述 = "单位组_为单位组添加单位";
    public string 变量名;
    public int 添加的单位类型;


    public override void 数据项目新增(string type)
    {
    }

    public override void 渲染()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(动作描述, GUILayout.Width(200));

            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("变量名"));
            变量名 = EditorGUILayout.TextField("变量名", 变量名, GUILayout.Width(150));
            添加的单位类型 = EditorGUILayout.Popup("", 添加的单位类型, G.变量函数_单位Array, GUILayout.MaxWidth(150));
        }
        EditorGUILayout.EndHorizontal();

    }
}