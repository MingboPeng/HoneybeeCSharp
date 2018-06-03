﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ironbug.HVAC.BaseClass;
using OpenStudio;

namespace Ironbug.HVAC
{
    public class IB_FanVariableVolume : IB_Fan
    {
        protected override Func<IB_ModelObject> IB_InitSelf => () => new IB_FanVariableVolume();

        private static FanVariableVolume InitMethod(Model model) => new FanVariableVolume(model);
        public IB_FanVariableVolume() : base(InitMethod(new Model()))
        {
            
        }   

        protected override ModelObject InitOpsObj(Model model)
        {
            return base.OnInitOpsObj(InitMethod, model).to_FanVariableVolume().get();
        }

        public override bool AddToNode(Node node)
        {
            var model = node.model();
            return ((FanVariableVolume)this.ToOS(model)).addToNode(node);
        }
    }

    public sealed class IB_FanVariableVolume_DataFields 
        : IB_FieldSet<IB_FanVariableVolume_DataFields, FanVariableVolume>
    {
        
        private IB_FanVariableVolume_DataFields() {}

        public IB_Field Name { get; }
            = new IB_BasicDataField("Name", "Name");

        public IB_Field FanEfficiency { get; }
            = new IB_BasicDataField("FanEfficiency", "Efficiency");

        public IB_Field PressureRise { get; }
            = new IB_BasicDataField("PressureRise", "PressureRise");

        public IB_Field MotorEfficiency { get; }
            = new IB_ProDataField("MotorEfficiency", "MotorEfficiency");

        public IB_Field FanPowerCoefficient1 { get; }
            = new IB_ProDataField("FanPowerCoefficient1", "Coefficient1");
        public IB_Field FanPowerCoefficient2 { get; }
            = new IB_ProDataField("FanPowerCoefficient2", "Coefficient2");
        public  IB_Field FanPowerCoefficient3 { get; }
            = new IB_ProDataField("FanPowerCoefficient3", "Coefficient3");
        public IB_Field FanPowerCoefficient4 { get; }
            = new IB_ProDataField("FanPowerCoefficient4", "Coefficient4");
        public IB_Field FanPowerCoefficient5 { get; }
            = new IB_ProDataField("FanPowerCoefficient5", "Coefficient5");
        


    }
}
