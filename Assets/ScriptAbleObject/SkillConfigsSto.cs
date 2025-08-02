using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillConfigsSto : ScriptableObject
{
    public int 技能ID;
    public string 技能名称;
    public string 技能描述;
    public int 目标检测类型;
    public int 技能目标类型;
    public string icon;
    public int 技能类型;
    public float CD时间;
    public int 技能指示器形状;
    public float 范围参数1;
    public float 范围参数2;
    public int 资源消耗类型;
    public float 资源消耗数量;
    public int 升级所需等级;
    public bool 面向施法对象;
    public bool 释放时可转向;
    public bool 释放时可移动;

    // 动画
    public string 模型name;
    public string 使用动画片段;
    public List<G.跳转条件> 跳转条件list = new List<G.跳转条件>();
    

}
