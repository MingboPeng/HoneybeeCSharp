﻿using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Ironbug.Grasshopper.Properties;
using Rhino.Geometry;

namespace Ironbug.Grasshopper.Component.Ironbug
{
    public class Ironbug_FanVariableVolume : Ironbug_HVACComponent
    {

        /// <summary>
        /// Initializes a new instance of the Ironbug_FanVariableVolume class.
        /// </summary>
        public Ironbug_FanVariableVolume()
          : base("Ironbug_FanVariableVolume", "FanVariable",
              "Description",
              "Ironbug", "01:LoopComponents",
              typeof(HVAC.IB_FanVariableVolume_DataFields))
        {
        }
        
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Parameters for Fan:VariableVolume", "params_", "Detail settings for this fan. Use Ironbug_ObjParams to set this.", GH_ParamAccess.item);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("FanVariableVolume", "Fan", "connect to airloop's supply side", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var obj = new HVAC.IB_FanVariableVolume();

            var settingParams = new Dictionary<HVAC.IB_DataField, object>();
            DA.GetData(0, ref settingParams);

            obj.SetAttributes(settingParams);

            DA.SetData(0, obj);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Resources.FanV;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("eebe83e8-f84d-492a-8394-0b81ab2002e0"); }
        }
    }
}