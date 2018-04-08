﻿using Ironbug.HVAC.BaseClass;
using OpenStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ironbug.HVAC.SetpointManagers
{
    public class IB_SetpointManagerScheduled : IB_SetpointManager
    {
        private double temperature = 12.7778; //55F
        private  SetpointManagerScheduled InitMethod(Model model) 
            => new SetpointManagerScheduled(model, new ScheduleCompact(model, this.temperature));
        private static SetpointManagerScheduled InitMethod(Model model, double temp) 
            => new SetpointManagerScheduled(model, new ScheduleCompact(model, temp));

        public IB_SetpointManagerScheduled(double temperature) : base(InitMethod(new Model(), temperature))
        {
            this.temperature = temperature;
        }
        public override bool AddToNode(Node node)
        {
            var model = node.model();

            return ((SetpointManagerScheduled)this.ToOS(model)).addToNode(node);
        }

        public override IB_ModelObject Duplicate()
        {
            return base.DuplicateIBObj(() => new IB_SetpointManagerScheduled(this.temperature));
        }

        public override ModelObject ToOS(Model model)
        {
            return base.ToOS(InitMethod, model).to_SetpointManagerScheduled().get();
        }
    }
}