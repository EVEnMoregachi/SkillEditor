using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


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

    public static void RemoceWindow(EditorWindowBase window)
    {
        重复窗口优先级--;
        window.优先级 = 重复窗口优先级;
        if (!windowList.Contains(window))
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
    public static void destroyWindow()
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
        windowList.Sort((x, y) => x.优先级.CompareTo(y.优先级));
    }
}
