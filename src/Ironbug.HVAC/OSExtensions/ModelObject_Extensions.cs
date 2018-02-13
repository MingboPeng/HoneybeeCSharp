﻿using OpenStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ironbug.HVAC
{
    public static class ModelObjectExtensions
    {
        public static string OSType(this ModelObject component)
        {
            return component.iddObjectType().valueDescription();
        }

        public static bool IsNode(this ModelObject component)
        {
            return component.OSType() == "OS:Node";
        }

        public static bool IsNotInModel(this ParentObject component, Model model)
        {
            return model.getParentObjectByName(component.nameString()).isNull();
        }

        public static object GetAttributeValue(this ModelObject component, string getterMethodName)
        {
            string methodName = getterMethodName;

            var method = component.GetType().GetMethod(methodName, new Type[] { });
            var invokeResult = method.Invoke(component, null);

            return invokeResult;
        }
        public static object SetCustomAttribute(this ModelObject component, string setterMethodName, object AttributeValue)
        {

            string methodName = setterMethodName;
            object[] parm = new object[] { AttributeValue };

            var method = component.GetType().GetMethod(methodName, new[] { AttributeValue.GetType() });
            var invokeResult = method.Invoke(component, parm);

            return invokeResult;
        }

        public static List<string> SetCustomAttributes(this ModelObject component, Dictionary<string, object> dataField)
        {
            var invokeResults = new List<string>();
            foreach (var item in dataField)
            {
                var name = item.Key;
                var invokeResult = component.SetCustomAttribute(name, item.Value);

                invokeResults.Add(name + " :: " + invokeResult);
            }

            return invokeResults;
        }

        public static string CheckName(this ModelObject component)
        {
            var name = component.nameString();
            var NewName = CheckString(name);
            if (name != NewName)
            {
                component.setName(NewName);
            }

            return NewName;
        }
        public static string CheckName(this ModelObject component, string NewName)
        {
            var name = CheckString(NewName);

            if (name != NewName)
            {
                component.setName(name);
            }


            return name;
        }
        private static string CheckString(string name)
        {
            var idKey = "[#";
            if (!name.Contains(idKey))
            {
                var uid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("/", "").Replace("+", "").Substring(0, 6)+"]";
                name = name + idKey + uid;
            }

            return name;
        }
    }
}
