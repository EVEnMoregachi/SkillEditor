using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


internal class EG
{
    public class 单位组Bass
    {
        public virtual void 渲染() { }

    }

    public static float calcLabelWidth(GUIContent label)
    {
        return GUI.skin.label.CalcSize(label).x + EditorGUI.indentLevel * GUI.skin.label.fontSize * 2;
    }

    public static Dictionary<Vector2, string> 动作类字典 = new Dictionary<Vector2, string>();

    public static string 根据key获取动作类(Vector2 key)
    {
        if (动作类字典.Count == 0)
            初始化字典();
        if (动作类字典.ContainsKey(key))
        {
            return 动作类字典[key];
        }
        return "";
    }

    private static void 初始化字典()
    {   // 这里的第一个值对应Gloable.cs中行为类型Array的值，第二个值对应二级菜单的值
        动作类字典.Add(new Vector2(5, 0), "计时器动作");
        动作类字典.Add(new Vector2(4, 0), "技能_造成伤害");
        动作类字典.Add(new Vector2(0, 0), "判断动作");
    }

    public class 单位组_随机单位组 : 单位组Bass
    {
        public int 选择的单位组;// 下拉选项索引
        public 单位组Bass 选择单位组;
        public int 随机抽取num;

        public override void 渲染()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUI.indentLevel++;
                {
                    EditorGUI.BeginChangeCheck(); // 检测是否有改变
                    {
                        选择的单位组 = EditorGUILayout.Popup("选择的单位组", 选择的单位组, G.单位组选择_Array, GUILayout.MaxWidth(350));
                    }
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (选择的单位组 == 0)
                        {
                            选择单位组 = new 单位组_范围内的所有单位();
                        }
                        else if (选择的单位组 == 1)
                        {
                            选择单位组 = new EG.单位组_随机单位组();
                        }
                    }
                    选择单位组!.渲染(); // 可以进行嵌套渲染
                    随机抽取num = EditorGUILayout.IntField("随机抽取", 随机抽取num, GUILayout.MaxWidth(350));
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndHorizontal();
        }
    }


    public class 单位组_范围内的所有单位 : 单位组Bass
    {
        public int 选择的单位;
        public float 半径;

        public override void 渲染()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUI.indentLevel++;
                {
                    选择的单位 = EditorGUILayout.Popup("选择的单位", 选择的单位, G.变量函数_单位Array, GUILayout.MaxWidth(350));
                    半径 = EditorGUILayout.FloatField("半径", 半径, GUILayout.MaxWidth(350));
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();
        }
    }

}

public interface I动作
{
    public void 渲染();
    public void 数据项目新增(string type);

}

public abstract class 动作 : I动作
{
    public int 动作类型;
    public int 具体动作;
    public abstract void 数据项目新增(string type);
    public abstract void 渲染();


}

public class 创建计时器info
{
    public int 计时器类型;
    public float 计时周期;
    public List<动作> 动作list;
}