﻿using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Ironbug.Grasshopper.Properties;
using Ironbug.HVAC.BaseClass;
using Rhino.Geometry;

namespace Ironbug.Grasshopper.Component
{
    public class Ironbug_PlantLoop_DW : Ironbug_HVACComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the Ironbug_PlantLoop class.
        /// </summary>
        public Ironbug_PlantLoop_DW()
          : base("Ironbug_CondenserWaterPlantLoop", "DWLoop",
              "Same as PlantLoop, except the FluidType and LoopType cannot be overridden.",
              "Ironbug", "01:Loops",
              typeof(HVAC.IB_PlantLoop_DataFieldSet))
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("supply", "supply", "HVAC components", GH_ParamAccess.list);
            pManager[0].Optional = true;
            pManager.AddGenericParameter("demand", "demand", "HVAC components", GH_ParamAccess.list);
            pManager[1].Optional = true;
            pManager.AddGenericParameter("sizingLoop", "sizing", "HVAC components", GH_ParamAccess.item);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("CondenserWaterPlantLoop", "DWLoop", "CondenserWaterPlantLoop", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<IB_HVACObject> supplyComs = new List<IB_HVACObject>();
            List<IB_HVACObject> demandComs = new List<IB_HVACObject>();
            HVAC.IB_SizingPlant sizing = new HVAC.IB_SizingPlant();

            DA.GetDataList(0, supplyComs);
            DA.GetDataList(1, demandComs);
            DA.GetData(2, ref sizing);


            var plant = new HVAC.IB_PlantLoop();
            foreach (var item in supplyComs)
            {
                var newItem = (IB_HVACObject)item.Duplicate();
                plant.AddToSupply(newItem);
            }
            foreach (var item in demandComs)
            {
                var newItem = (IB_HVACObject)item.Duplicate();
                plant.AddToDemand(newItem);
            }
            
            var sizingChecked = this.setSizingDefault(sizing);
            plant.SetSizingPlant(sizingChecked);
            
            base.SetObjParamsTo(plant);
            

            var plantFields = HVAC.IB_PlantLoop_DataFieldSet.Value;
            if (!plant.CustomAttributes.ContainsKey("setName"))
            {
                plant.SetFieldValue(plantFields.Name, "Condenser Water Loop");
            }
            plant.SetFieldValue(plantFields.FluidType, "Water");
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
                return Resources.PlantLoopDW;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("599496DE-2192-4790-9C47-A8FC8F3F9913"); }
        }

        private HVAC.IB_SizingPlant setSizingDefault(HVAC.IB_SizingPlant sizingPlant)
        {
            var szFields = HVAC.IB_SizingPlant_DataFieldSet.Value;
            var sizing = sizingPlant.Duplicate() as HVAC.IB_SizingPlant;

            var custAtt = sizing.CustomAttributes;

            sizing.SetFieldValue(szFields.LoopType, "Condenser");

            
            
            if (!custAtt.ContainsKey("DesignLoopExitTemperature"))
            {
                sizing.SetFieldValue(szFields.DesignLoopExitTemperature, 29.4);
            }

            if (!custAtt.ContainsKey("LoopDesignTemperatureDifference"))
            {
                sizing.SetFieldValue(szFields.LoopDesignTemperatureDifference, 5.6);
            }
            

            return sizing;
        }

        public override void CreateAttributes()
        {
            var newAttri = new IB_LoopComponentAttributes(this);
            m_attributes = newAttri;
        }
    }
}