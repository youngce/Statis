using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace StatisticsMethods
{
    public class IntervalEstimate
    {

        public static IntervalEstimateInformation ConfidenceIntervalOfMean(IEnumerable<double> data, double confidenceLevel=0.95,  bool? isRightWay=null)
        {
            IntervalEstimateInformation ciInformation=new IntervalEstimateInformation();
            ciInformation.ConfideceLevel = confidenceLevel;
            double alpha=  confidenceLevel;

            DescriptiveStatistics stat = new DescriptiveStatistics(data);
            int df = stat.Count - 1;
            if (stat.Count < 2)
                return ciInformation;
            if (isRightWay == null)
            {
                alpha = 1-(1 - confidenceLevel)/2d;
            }

            double? confidenceCoeff;
           // var normal = new Normal();
            
            var  studentT = new StudentT(0,1,df);

            confidenceCoeff = MathDotNetAdditional.Dichotomy(studentT, alpha);
            if (isRightWay == null)
            {
                ciInformation.Upper = stat.Mean + confidenceCoeff*stat.StandardDeviation/Math.Sqrt(df);
                ciInformation.Lower = stat.Mean - confidenceCoeff * stat.StandardDeviation / Math.Sqrt(df);
            }
            if (isRightWay==true)
                ciInformation.Upper = stat.Mean + confidenceCoeff * stat.StandardDeviation / Math.Sqrt(df);
            if(isRightWay==false)
                ciInformation.Lower = stat.Mean - confidenceCoeff * stat.StandardDeviation / Math.Sqrt(df);

            return ciInformation;

        }
       
    }
    public class IntervalEstimateInformation
    {
        public double ConfideceLevel { get; set; }
        public double? Upper { get; set; }
        public double? Lower { get; set; }
    }
}
