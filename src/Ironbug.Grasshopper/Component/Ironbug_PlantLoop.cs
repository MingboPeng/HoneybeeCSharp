﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Ironbug.Grasshopper.Component
{
    public class Ironbug_PlantLoop : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Ironbug_PlantLoop class.
        /// </summary>
        public Ironbug_PlantLoop()
          : base("Ironbug_PlantLoop", "Nickname",
              "Description",
              "Ironbug", "02:Loops")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("demand", "demand", "HVAC components", GH_ParamAccess.list);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("PlantLoop", "PlantLoop", "PlantLoop", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<HVAC.IB_HVACComponent> demandComs = new List<HVAC.IB_HVACComponent>();
            DA.GetDataList(0, demandComs);


            var plant = new HVAC.IB_PlantLoop();
            foreach (var item in demandComs)
            {
                plant.AddToDemandBranch(item);
            }

            DA.SetData(0, plant);
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
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("63e4f976-4b63-48e4-b6f7-a2b5d7040252"); }
        }
    }
}