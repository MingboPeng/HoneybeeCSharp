﻿namespace Ironbug.HVAC.BaseClass
{
    public class IB_OutputVariable
    {
        public string VariableName { get; private set; }
        public string TimeStep { get; private set; }

        public IB_OutputVariable(string variableName, TimeSteps timeStep)
        {
            this.VariableName = variableName;
            this.TimeStep = timeStep.ToString();

        }

        public bool ToOS(OpenStudio.Model model, string keyName)
        {
            var outV = new OpenStudio.OutputVariable(this.VariableName, model);
            var success = outV.setReportingFrequency(this.TimeStep);
            success &= outV.setKeyValue(keyName);
            return success;
        }

        public enum TimeSteps
        {
            Detail,
            Hourly,
            Daily,
            Monthly,
            RunPeriod
        }
    }
}