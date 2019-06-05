﻿using System;
using Grasshopper.Kernel;

namespace Ironbug.Grasshopper.Component
{
    public class Ironbug_CoilHeatingDXSingleSpeed : Ironbug_HVACComponent
    {
        public Ironbug_CoilHeatingDXSingleSpeed()
          : base("Ironbug_CoilHeatingDXSingleSpeed", "CoilHtn_DX1",
              "Description",
              "Ironbug", "02:LoopComponents",
              typeof(HVAC.IB_CoilHeatingDXSingleSpeed_FieldSet))
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.secondary;
        
        
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("CoilHeatingDXSingleSpeed", "Coil", "CoilHeatingDXSingleSpeed", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var obj = new HVAC.IB_CoilHeatingDXSingleSpeed();
            var objs = this.SetObjParamsTo(obj);
            DA.SetDataList(0, objs);
        }
        
        protected override System.Drawing.Bitmap Icon => Properties.Resources.CoilHDX1;
        
        public override Guid ComponentGuid => new Guid("9C0338B1-C2FE-4AEC-8219-BCF87128BF5D");
    }
}