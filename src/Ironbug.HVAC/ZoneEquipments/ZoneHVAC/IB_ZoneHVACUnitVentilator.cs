﻿using Ironbug.HVAC.BaseClass;
using OpenStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ironbug.HVAC
{
    public class IB_ZoneHVACUnitVentilator : IB_ZoneEquipment
    {
        private static ZoneHVACUnitVentilator InitMethod(Model model) => new ZoneHVACUnitVentilator(model);

        public IB_ZoneHVACUnitVentilator() : base(InitMethod(new Model()))
        {
        }

        public override IB_ModelObject Duplicate()
        {
            return base.DuplicateIBObj(() => new IB_ZoneHVACUnitVentilator());
        }

        public override ModelObject ToOS(Model model)
        {
            return base.ToOS(InitMethod, model).to_ZoneHVACUnitVentilator().get();
        }
    }

    public sealed class IB_ZoneHVACUnitVentilator_DataFieldSet 
        : IB_DataFieldSet<IB_ZoneHVACUnitVentilator_DataFieldSet, ZoneHVACUnitVentilator>
    {
        private IB_ZoneHVACUnitVentilator_DataFieldSet() {}

    }
}