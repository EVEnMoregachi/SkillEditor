using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 变量_设置局部变量_单位组 : 动作
{
    public string 动作描述 = "变量_设置局部变量_单位组";
    public string 变量名;
    public int 创建数据类型;
    public override void 数据项目新增(string type)
    {

    }

    public override void 渲染()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(动作描述, GUILayout.Width(200));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("变量名"));
            变量名 = EditorGUILayout.TextField("变量名", 变量名, GUILayout.MaxWidth(200));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("创建数据类型"));
            创建数据类型 = EditorGUILayout.Popup("", 创建数据类型,G.单位组选择_Array, GUILayout.MaxWidth(150));
        }
        EditorGUILayout.EndHorizontal();
    }
}