﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ironbug.HVAC
{
    public class IB_DataField
    {
        public string FullName { get; private set; }
        public string PerfectName { get; private set; }
        public string ShortName { get; private set; }
        public string GetterMethodName { get; private set; }
        public string SetterMethodName { get; private set; }
        public Type DataType { get; private set; }
        public bool IsBasicSetting { get; private set; }
        public bool IsHidden { get; set; }

        public List<object> ValidData { get; private set; }
        public string Description { get; set; }
        //public string Unit { get; set; }

        public IB_DataField(string FullName, string ShortName, Type DataType)
            : this(FullName, ShortName, DataType, false, new List<object>())
        {
        }

        public IB_DataField(string FullName, string ShortName, Type DataType, bool BasicSetting)
            : this(FullName, ShortName, DataType, BasicSetting, new List<object>())
        {
        }

        public IB_DataField(string FullName, string ShortName, Type DataType, List<object> ValidData)
            : this(FullName, ShortName, DataType, false, ValidData)
        {
        }

        public IB_DataField(string FullName, string ShortName, Type DataType, bool BasicSetting, List<object> ValidData)
        {
            this.FullName = FullName; //RatedInletWaterTemperature
            this.ShortName = ShortName; //InWaterTemp

            this.PerfectName = CheckName(this.FullName); ////Rated Inlet Water Temperature
            this.GetterMethodName = Char.ToLowerInvariant(this.FullName[0]) + this.FullName.Substring(1); //ratedInletWaterTemperature
            this.SetterMethodName = "set" + this.FullName;

            //this.Type = com.GetType().GetMethod(methodName).ReturnType;
            this.DataType = DataType;
            this.IsBasicSetting = BasicSetting;
            this.ValidData = ValidData;
        }

        public void AddDataFieldDescription(string Description)
        {
            this.Description = Description;
        }

        private static string CheckName(string LongName)
        {
            var r = new System.Text.RegularExpressions.Regex(@"(?<=[A-Z])(?=[A-Z][a-z]) |(?<=[^A-Z])(?=[A-Z]) |(?<=[A-Za-z])(?=[^A-Za-z])", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);
            return r.Replace(LongName, " ");
        }

        public string Unit(bool IP = false)
        {
            return "ddd";
        }

    }
    
}