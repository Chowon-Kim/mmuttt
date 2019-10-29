using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Samsung.AppCommon.MVVM.Utils
{
    public class VisualTreeUtils
    {
        public static T GetParentByType<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj == null)
                return null;

            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if (parent.GetType() == typeof(T))
                    break;
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (parent == null ? null : (T)parent);
        }

        public static T GetChildByType<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj == null)
                return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(obj, i);
                if (typeof(T) == current.GetType())
                {
                    return (T)current;
                }
                else
                {
                    T next = GetChildByType<T>(current);
                    if (next != null)
                        return next;
                }
            }

            return null;
        }

        public static T GetChildByAsType<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj == null)
                return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(obj, i);
                if ((current as T) != null)
                {
                    return current as T;
                }
                else
                {
                    T next = GetChildByAsType<T>(current);
                    if (next != null)
                        return next;
                }
            }

            return null;
        }
        
        public static T GetChildByName<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            if (obj == null)
                return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childrenCount; i++)
            {
                FrameworkElement current = VisualTreeHelper.GetChild(obj, i) as FrameworkElement;
                if (current != null && current.Name == name && current.GetType() == typeof(T))
                {
                    return (T)current;
                }
                else
                {
                    T next = GetChildByName<T>(current, name);
                    if (next != null)
                        return next;
                }
            }

            return null;
        }

        public static FrameworkElement GetChildByName(DependencyObject obj, string name)
        {
            if (obj == null)
                return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childrenCount; i++)
            {
                FrameworkElement current = (FrameworkElement)VisualTreeHelper.GetChild(obj, i);
                if (current != null && current.Name == name)
                {
                    return current;
                }
                else
                {
                    FrameworkElement next = GetChildByName(current, name);
                    if (next != null)
                        return next;
                }
            }

            return null;
        }

        public static T GetSiblingByName<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            if (obj == null || string.IsNullOrEmpty(name))
                return null;

            // find parent
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            if (parent == null)
                return null;

            // find sibling
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                T sibling = VisualTreeHelper.GetChild(parent, i) as T;
                if (sibling != null && sibling.Name == name)
                    // found!
                    return sibling;
            }

            // not found
            return null;
        }

        public static FrameworkElement GetSiblingByName(DependencyObject obj, string name)
        {
            if (obj == null || string.IsNullOrEmpty(name))
                return null;

            // find parent
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            if (parent == null)
                return null;

            // find sibling
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                FrameworkElement sibling = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (sibling != null && sibling.Name == name)
                    // found!
                    return sibling;
            }

            // not found
            return null;
        }

        public static void PrintAllNodes_Debug(FrameworkElement root)
        {
            Debug.WriteLine("========================== PrintAllNodes_Debug() Start");

            Queue<KeyValuePair<FrameworkElement, int>> queue = new Queue<KeyValuePair<FrameworkElement, int>>();
            int level = 0;

            // enqueue
            queue.Enqueue(new KeyValuePair<FrameworkElement, int>(root, level));

            while (queue.Count != 0)
            {
                // dequeue
                KeyValuePair<FrameworkElement, int> pair = queue.Dequeue();
                FrameworkElement current = pair.Key;

                if (current == null) continue;

                // print type and name
                Debug.WriteLine(string.Format("[{2}] Type : {0}, Name : {1}", current.GetType().ToString(), current.Name, pair.Value));

                // enqueu children
                level++;
                int count = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < count; i++)
                {
                    FrameworkElement child = VisualTreeHelper.GetChild(current, i) as FrameworkElement;
                    if (child != null)
                        queue.Enqueue(new KeyValuePair<FrameworkElement, int>(child, level));
                }
            }

            Debug.WriteLine("========================== PrintAllNodes_Debug() End");
        }
    }
}
