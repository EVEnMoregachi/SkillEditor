using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 变量_设置全局变量_整数 : 动作
{
    public string 动作描述 = "变量_设置全局变量_整数";
    public string 变量名;
    public int value;
    public string 赋值变量;
    public int 数值选项参数;
    public int 使用函数类型;
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

            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("数值选项"));
            EditorGUI.BeginChangeCheck();
            {
                数值选项参数 = EditorGUILayout.Popup("数值选项", 数值选项参数, G.数值选项参数Array, GUILayout.MaxWidth(200));
            }
            if (数值选项参数 == 0)
            {
                EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("值"));
                value = EditorGUILayout.IntField("值", value, GUILayout.MaxWidth(150));
            }
            else if(数值选项参数 == 1)
            {
                EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("变量名"));
                赋值变量 = EditorGUILayout.TextField("变量名", 赋值变量, GUILayout.MaxWidth(150));
            }
            else if (数值选项参数 == 2)
            {
                EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("函数选择"));
                EditorGUI.BeginChangeCheck();
                {
                    使用函数类型 = EditorGUILayout.Popup("函数选择", 使用函数类型, G.变量函数_整数Array, GUILayout.MaxWidth(200));
                }
                if (EditorGUI.EndChangeCheck())
                {

                }
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}