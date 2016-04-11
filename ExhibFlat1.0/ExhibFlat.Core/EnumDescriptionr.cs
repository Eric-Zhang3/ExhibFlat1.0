using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace ExhibFlat.Core
{

    public sealed class EnumDescription 
    {
        /// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>        
        public static string GetEnumDescription(Enum enumSubitem)
        {
            Object obj = GetAttributeClass(enumSubitem, typeof(DescriptionAttribute));
            if (obj == null)
            {
                return enumSubitem.ToString();
            }
            else
            {
                DescriptionAttribute da = (DescriptionAttribute)obj;
                return da.Description;
            }
        }

        public static void GetselfAttributeInfo(Enum enumSubitem, out string text, out string test)
        {
            Object obj = GetAttributeClass(enumSubitem, typeof(selfAttribute));
            if (obj == null)
            {
                text = test = enumSubitem.ToString();
            }
            else
            {
                selfAttribute da = (selfAttribute)obj;
                text = da.DisplayText;
                test = da.DisplayTest;
            }
        }

        /// <summary>
        /// 获取指定属性类的实例
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>
        /// <param name="attributeType">DescriptionAttribute属性类或其自定义属性类 类型，例如：typeof(DescriptionAttribute)</param>
        private static Object GetAttributeClass(Enum enumSubitem, Type attributeType)
        {
            FieldInfo fieldinfo = enumSubitem.GetType().GetField(enumSubitem.ToString());
            Object[] objs = fieldinfo.GetCustomAttributes(attributeType, false);
            if (objs == null || objs.Length == 0)
            {
                return null;
            }
            return objs[0];
        }

    }

    /// <summary>
    /// 自定义的一个属性类
    /// </summary>
    public class selfAttribute : Attribute
    {
        public selfAttribute(string displayText, string displayTest)
        {
            m_DisplayText = displayText;
            m_DisplayTest = displayTest;
        }

        private string m_DisplayText = string.Empty;
        private string m_DisplayTest = string.Empty;
        public string DisplayText
        {
            get { return m_DisplayText; }
        }

        public string DisplayTest
        {
            get { return m_DisplayTest; }
        }
    }


   
} 
