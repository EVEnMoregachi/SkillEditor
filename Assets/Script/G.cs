using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G
{
    public class 跳转条件
    {
        public int 目标动画;
        public int 开始帧;
        public int 结束帧;
        public List<int> 操作 = new List<int>();

    }

    public class 攻击判定
    {
        public int 帧号;
        public bool 是否重写范围;
        public int 判定形状;
        public float 参数1;
        public float 参数2;
        public Vector3 offset;

    }

    public class 特效音效信息
    {
        public int 帧号;
        public ParticleSystem 粒子特效;
        public string 特效ID;
        public Vector3 offset;
        public AudioClip 音效;
    }

    public static string[] 属性Array = new string[]
    {
        "攻击加成",
        "攻速加成",
        "移速加成",
        "生命恢复",
        "暴击率",
        "暴击伤害",
        "冷却加速",
    };

    public class buff属性影响
    {
        public int 影响属性_;
        public float 数值1;
        public float 数值2;
        public int 计算属性_;
    }

    public static string[] 特效挂接点Array = new string[]
    {
        "脚底",
        "腰部",
        "胸部",
        "左手",
        "右手",
        "头部",
    };
    public class buff特效信息
    {
        public int 特效ID;
        public int 特效挂接点;
        public float 缩放系数;

    }

    public static string[] 伤害类别Array = new string[]
    {
        "物理伤害",
        "魔法伤害",
        "真实伤害",
        "消耗法力",
        "恢复生命",
        "恢复法力",
    };

    public static string[] buff数值计算类别Array = new string[]
    {
        "物攻",
        "法攻",
        "护甲",
        "最大生命值",
        "当前生命值",
        "等级",
    };

    public class buff伤害信息
    {
        public int 伤害类别_;
        public float 数值a1;
        public float 数值a2;
        public int buff数值计算类别a;

        public float 数值b1;
        public float 数值b2;
        public int buff数值计算类别b;

        public float 数值c1;
        public float 数值c2;
        public int buff数值计算类别c;

        public bool 是否可暴击;
    }

}
