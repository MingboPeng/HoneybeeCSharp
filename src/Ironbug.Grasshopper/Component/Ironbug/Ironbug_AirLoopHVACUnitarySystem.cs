﻿using System;
using Grasshopper.Kernel;
using Ironbug.HVAC;

namespace Ironbug.Grasshopper.Component
{
    public class Ironbug_AirLoopHVACUnitarySystem : Ironbug_HVACComponent
    {
        public Ironbug_AirLoopHVACUnitarySystem()
          : base("Ironbug_AirLoopHVACUnitarySystem", "UnitarySystem",
              "Description",
              "Ironbug", "02:LoopComponents",
              typeof(IB_AirLoopHVACUnitarySystem_FieldSet))
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.primary | GH_Exposure.obscure;

        protected override System.Drawing.Bitmap Icon => Properties.Resources.PTHP;

        public override Guid ComponentGuid => new Guid("{3E1B31B0-D5D2-4DBD-95C9-669A8A7D8764}");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("HeatingCoil", "coilH_", "Heating coil to provide heating source. By default, no heating coil is included.", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddGenericParameter("CoolingCoil", "coilC_", "Cooling coil to provide cooling source. By default, no cooling coil is included. ", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddGenericParameter("Fan", "fan_", "Supply fan. By default, FanOnOff is included.", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddGenericParameter("SupplementalHeatingCoil", "spCoilH_", "SupplementalHeatingCoil. By default, no supplemental heating coil is included.", GH_ParamAccess.item);
            pManager[3].Optional = true;
        }
        
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("UnitarySystem", "UniSys", "Connect to airloop's supply side", GH_ParamAccess.item);
        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {

            HVAC.BaseClass.IB_Fan fan = null;
            HVAC.BaseClass.IB_Coil coilH = null;
            HVAC.BaseClass.IB_Coil coilC = null;
            HVAC.BaseClass.IB_Coil spCoilH = null;

            var obj = new HVAC.IB_AirLoopHVACUnitarySystem();

            if (DA.GetData(0, ref coilH)) obj.SetHeatingCoil(coilH);
            if (DA.GetData(1, ref coilC)) obj.SetCoolingCoil(coilC);
            if (DA.GetData(2, ref fan)) obj.SetFan(fan);
            if (DA.GetData(3, ref spCoilH)) obj.SetSupplementalHeatingCoil(spCoilH);

            

            this.SetObjParamsTo(obj);
            DA.SetData(0, obj);
        }
        
    }
}