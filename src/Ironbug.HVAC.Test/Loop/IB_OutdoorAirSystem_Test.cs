﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ironbug.HVACTests.Loop
{
    [TestClass]
    public class IB_OutdoorAirSystem_Test
    {
        [TestMethod]
        public void IB_OutdoorAirSystem_Initialize_Test()
        {
            var obj = new HVAC.IB_OutdoorAirSystem();
            var dataFields = obj.GetDataFields();
            Assert.IsTrue(dataFields.Count() == 1);
        }

        [TestMethod]
        public void IB_OutdoorAirSystem_SetController_Test()
        {
            var model = new OpenStudio.Model();
            var loop = new OpenStudio.AirLoopHVAC(model);

            var obj = new HVAC.IB_OutdoorAirSystem();
            var ctrl = new HVAC.IB_ControllerOutdoorAir();

            var testValue = 0.01;
            ctrl.SetAttribute(HVAC.IB_ControllerOutdoorAir_DataFieldSet.MinimumOutdoorAirFlowRate, testValue);
            obj.AddController(ctrl);
            obj.AddToNode(model, loop.supplyOutletNode());

            var inSysCtrl = model.getAirLoopHVACOutdoorAirSystems().First().getControllerOutdoorAir();
            var att = inSysCtrl.minimumOutdoorAirFlowRate();

            Assert.IsTrue(att.get() == testValue);
        }
    }
}