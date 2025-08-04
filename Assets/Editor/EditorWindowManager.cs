using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


internal class EditorWindowManager
{
    private static List<EditorWindowBase> windowList = new List<EditorWindowBase>();
    private static int 重复窗口优先级 = 10;
    public static void add重复窗口(EditorWindowBase window)
    {
        重复窗口优先级++;
        window.优先级 = 重复窗口优先级;
        if (!windowList.Contains(window))
        {
            windowList.Add(window);
            SortWindowList();
        }
    }

    public static void Remove重复窗口(EditorWindowBase window)
    {
        重复窗口优先级--;
        window.优先级 = 重复窗口优先级;
        if (windowList.Contains(window))
        {
            windowList.Remove(window);
            SortWindowList();
        }
    }

    public static void focusWindow()
    {
        if (windowList.Count > 0)
        {
            windowList[windowList.Count - 1].Focus();
            
        }
    }

    /// <summary>
    /// 关闭所有弹窗
    /// </summary>
    public static void destroyAllWindow()
    {
        for (int i = 0; i < windowList.Count; i++)
        {
            if (windowList[i] != null)
            {
                windowList[i].Close();
            }
        }
        windowList.Clear();
    }

    private static void SortWindowList()
    {
        windowList.Sort((x, y) =>
        {
            return x.优先级.CompareTo(y.优先级);
        });
    }
}
