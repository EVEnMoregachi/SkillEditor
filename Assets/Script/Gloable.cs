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

    // ------------- buff start ------------------
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
    // ------------- buff end ------------------

    // ------------- 触发器 start -------------
    // 一级菜单
    public static string[] 行为类型Array = new string[]
    {
        "判断",
        "变量",
        "触发器",
        "单位组",
        "技能",
        "计时器",
        
    };
    // 二级级菜单
    public static string[] 判断Array = new string[]
    {
           "if|then|else",
    };

    public static string[] 变量Array = new string[]
    {
            "设置局部变量",
            "变量赋值",
    };

    public static string[] 触发器Array = new string[]
    {
            "创建触发器",
            "运行触发器",
    };

    public static string[] 单位组Array = new string[]
    {
           "为单位组添加单位",
    };

    public static string[] 技能Array = new string[]
    {
            "取消禁用技能",
            "禁用技能",
            "刷新技能冷却时间",
            "减少技能冷却时间",
            "重置技能冷却时间",
            "播放技能动画",
            "技能造成伤害",
            "技能造成治疗",
            "技能施加buff",
    };

    public static string[] 计时器Array = new string[]
    {
            "创建计时器",
            "运行计时器",
            "暂停计时器",
            "重置计时器",
            "设置计时器周期",
            "删除计时器",
    };

    public static string[] 条件判断1级参数Array = new string[]
    {
            "数值",

    };

    public static string[] 条件判断2级参数Array = new string[]
    {
            "整数判断",
            "浮点数判断",

    };

    public static string[] 数值关系Array = new string[]
    {
            "等于",
            "不等于",
            "大于",
            "小于",
            "大于等于",
            "小于等于",

    };

    public static string[] 数值选项参数Array = new string[]
    {
            "输入数值",
            "读取变量",
            "使用函数",
    };

    static List<string[]> 二级菜单 = new List<string[]>();
    public static Dictionary<string, List<string[]>> 根据ID选择二级菜单 = new Dictionary<string, List<string[]>>();

    public static void 初始化()
    {
        根据ID选择二级菜单.Clear();
        二级菜单.Clear();
        二级菜单.Add(判断Array);
        二级菜单.Add(变量Array);
        二级菜单.Add(触发器Array);
        二级菜单.Add(单位组Array);
        二级菜单.Add(技能Array);
        二级菜单.Add(计时器Array);
        根据ID选择二级菜单.Add("行为", 二级菜单);
    }
    // 三级菜单 
    public static string[] 变量类型Array = new string[]
    {
                "单位",
                "整数",
                "浮点数",
                "计时器",
                "触发器",
                "技能实例",
                "特效",
    };
    // 四级菜单 
    public static string[] 变量函数_单位Array = new string[]
    {
                    "任意单位",
                    "技能目标单位",
                    "技能释放单位",
                    "创建单位",
                    "范围最近单位",
                    "范围内血量最少的单位",
                    "范围内随机单位",
    };  

    public static string[] 变量函数_整数Array = new string[]
    {
                    "绝对值",
                    "相反数",
    };


    public static string[] 计时器类型_Array = new string[]
    {
                    "单词",
                    "循环",
    };  

    public static string[] 技能选择_Array = new string[]
    {
                    "触发技能",
                    "循环",
    };

    public static string[] 单位组选择_Array = new string[]
    {
                    "范围内所有单位",
                    "随机单位组",
                    "技能作用范围内的单位组",
    };

    public static string[] 指定点选择_Array = new string[]
    {
                    "指定范围内的随机点",
                    "单位的位置",
                    "技能的时放点",
                    "技能的目标点",
                    "玩家点击场景点",
    };

    public class 数值条件info
    {
        public int 数值关系;
        public int 数值1选项;
        public float 手动数值1;
        public int 数值2选项;
        public float 手动数值2;
    }

    public class 设置局部变量info
    {
        public int 变量类型;
        public string 变量名;
        public float 手动变量数值;
        public int 变量函数;
    }

    


    // ------------- 触发器 end -------------


}
