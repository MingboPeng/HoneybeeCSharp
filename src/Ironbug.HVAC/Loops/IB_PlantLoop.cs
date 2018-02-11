﻿using OpenStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ironbug.HVAC
{
    public class IB_PlantLoop:IB_ModelObject
    {
        private List<IB_HVACComponent> demandComponents { get; set; }

        public IB_PlantLoop()
        {
            this.demandComponents = new List<IB_HVACComponent>();
            this.ghostModelObject = new PlantLoop(new Model());
        }

        public void AddToDemandBranch(IB_HVACComponent HvacComponent)
        {
            this.demandComponents.Add(HvacComponent);
        }

        public void ToOS(ref Model model)
        {
            var plant = new PlantLoop(model);

            var boiler = new BoilerHotWater(model);
            plant.addSupplyBranchForComponent(boiler);

            
            foreach (var item in demandComponents)
            {
                plant.addDemandBranchForComponent(item.plantDemand());
            }

        }

    }
}
