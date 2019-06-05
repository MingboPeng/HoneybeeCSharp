﻿using System;
using Grasshopper.Kernel;

namespace Ironbug.Grasshopper.Component
{
    public class Ironbug_CoilHeatingWaterBaseboardRadiant : Ironbug_HVACComponent
    {
        public Ironbug_CoilHeatingWaterBaseboardRadiant()
          : base("Ironbug_CoilHeatingWaterBaseboardRadiant", "CoilHtn_BaseboardRad",
              "Description",
              "Ironbug", "04:ZoneEquipments",
              typeof(HVAC.IB_CoilHeatingWaterBaseboardRadiant_FieldSet))
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.quinary;

        
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("CoilHeatingWaterBaseboardRadiant", "Coil", "Connect to baseboard", GH_ParamAccess.item);
            pManager.AddGenericParameter("WaterSide_CoilHeatingWater", "ToWaterLoop", "Connect to hot water loop's demand side via plantBranches", GH_ParamAccess.item);
        }

        
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var obj = new HVAC.IB_CoilHeatingWaterBaseboardRadiant();
            

            var objs = this.SetObjParamsTo(obj);
            DA.SetDataList(0, objs);
            DA.SetDataList(1, objs);
        }


        protected override System.Drawing.Bitmap Icon => Properties.Resources.CoilHWBR;


        public override Guid ComponentGuid => new Guid("BAD43458-A637-43D1-A779-EC406A61128F");

    }
    
}