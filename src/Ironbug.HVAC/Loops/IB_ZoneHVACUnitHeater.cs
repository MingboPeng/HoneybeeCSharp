﻿using Ironbug.HVAC.BaseClass;
using OpenStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ironbug.HVAC
{
    public class IB_ZoneHVACUnitHeater : IB_ZoneEquipment
    {
        private static ZoneHVACUnitHeater InitMethod(Model model) 
            => new ZoneHVACUnitHeater(model,model.alwaysOnDiscreteSchedule(),new FanConstantVolume(model), new CoilHeatingElectric(model));

        private IB_Fan Fan { get; set; }
        private IB_Coil HeatingCoil { get; set; }

        public IB_ZoneHVACUnitHeater(): base(InitMethod(new Model()))
        {
            
        }

        public void AddFan(IB_Fan Fan)
        {
            this.Fan = Fan;
        }

        public void AddHeatingCoil(IB_Coil Coil)
        {
            //TODO: check if heating coil
            this.HeatingCoil = Coil;
        }

        public override IB_ModelObject Duplicate()
        {
            var newObj = (IB_ZoneHVACUnitHeater)base.DuplicateIBObj(() => new IB_ZoneHVACUnitHeater());
            newObj.AddFan((IB_Fan)this.Fan.Duplicate());
            newObj.AddHeatingCoil((IB_Coil)this.HeatingCoil.Duplicate());
            return newObj;

        }

        public override ModelObject ToOS(Model model)
        {
            var OsObj = base.ToOS(InitMethod, model).to_ZoneHVACUnitHeater().get();
            OsObj.setSupplyAirFan((HVACComponent)this.Fan.ToOS(model));
            OsObj.setHeatingCoil((HVACComponent)this.HeatingCoil.ToOS(model));
            return OsObj;
        }
    }
    public class IB_ZoneHVACUnitHeater_DataFieldSet : IB_DataFieldSet
    {
        private static ZoneHVACUnitHeater InitMethod(Model model)
            => new ZoneHVACUnitHeater(model, model.alwaysOnDiscreteSchedule(), new FanConstantVolume(model), new CoilHeatingElectric(model));
        protected override IddObject RefIddObject => InitMethod(new Model()).iddObject();

        protected override Type ParentType => typeof(ZoneHVACUnitHeater);

    }

}
