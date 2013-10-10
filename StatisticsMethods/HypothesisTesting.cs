using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;

namespace StatisticsMethods
{
    public class HypothesisTesting
    {
        private static double GetPValue(double nullValue,double testStat, IContinuousDistribution continuousDistribution,bool? isRightWay )
        {
          
            var pValue=2*(1-continuousDistribution.CumulativeDistribution(testStat));

            if (testStat < nullValue)
                pValue = 2 * continuousDistribution.CumulativeDistribution(testStat);
            
            if (isRightWay==true)
                pValue = (1 - continuousDistribution.CumulativeDistribution(testStat));
            if (isRightWay==false)
                pValue = continuousDistribution.CumulativeDistribution(testStat);

            return pValue;

        }
        private static void GetInformation(HypothesisTestingInformation information,double level,double pValue,double testStatistics)
        {
            var alpha = 1 - level;
            information.Level = level;
            information.PValue = pValue;

            information.IsRejectH0 = true;
            if (pValue > alpha)
                information.IsRejectH0 = false;
            
           information.TestStatistics = testStatistics;

        }
        public static HypothesisTestingInformation MeanTesting(IEnumerable<double> data,double level=0.95,double nullMean=0,bool? isRightWay=null)
        {
            HypothesisTestingInformation information=new HypothesisTestingInformation();

            BasicStatistics basicStat=new BasicStatistics(data);
            var alpha = 1 - level;
            var n = basicStat.Count;
            var mean = basicStat.Mean;
            var sd = basicStat.StandardDeviation;
            StudentT tDisbution=new StudentT(nullMean,1,n-1);
            var testingStat = (mean - nullMean)/(sd/Math.Sqrt(n));
            var pValue=GetPValue(nullMean,testingStat, tDisbution, isRightWay);
          
            GetInformation(information,level,pValue,testingStat);

            return information;
        }
        //private static IEnumerable<double> TwoDataError(IEnumerable<double> data1, IEnumerable<double> data2)
        //{
        //    IEnumerable<double> data=new double[data1.Count()];
        //    var arrayData1 = data1.ToArray();
        //    var arrauData2 = data2.ToArray();
        //    double[,]


        //    return data;
        //} 

        public static HypothesisTestingInformation MeanTesting(IEnumerable<double> data1, IEnumerable<double> data2, bool isPair = true, double level = 0.95, double nullMean = 0, bool? isRightWay = null)
        {
            HypothesisTestingInformation information = new HypothesisTestingInformation();

            BasicStatistics basicStat1 = new BasicStatistics(data1);
            BasicStatistics basicStat2 = new BasicStatistics(data2);

            

            var n1 = basicStat1.Count;
            var n2 = basicStat2.Count;
            //if (n1 != n2)
            //    isPair = false;
            
            //if (isPair == true)
            //{
                

            //}
            
            var variance1 = basicStat1.Variance;
            var variance2 = basicStat2.Variance;
            var mean1 = basicStat1.Mean;
            var mean2 = basicStat2.Mean;
            var mean = mean1 - mean2;

            bool isDiffVariance = VarianceTesting(data1, data2).IsRejectH0;
            
            double sd = Math.Sqrt(variance1/n1+variance2/n2);
            double df = (Math.Pow(variance1/n1 + variance2/n2, 2)/
                                     (Math.Pow(variance1/n1, 2)/(n1 - 1) + Math.Pow(variance2/n2, 2)/(n2 - 1)));
            if (isDiffVariance == false)
            {
                var spSquare = ((n1 - 1)*variance1 + (n2 - 1)*variance2)/(n1 + n2 - 2);
                sd = Math.Sqrt(spSquare * (1 / (double)n1 + 1 / (double)n2));
                df = n1 + n2 - 2;
            }
            
            StudentT tDisbution = new StudentT(nullMean, 1, df);
            var testingStat = (mean - nullMean) / sd;
            var pValue = GetPValue(nullMean,testingStat, tDisbution, isRightWay);
            
          
            GetInformation(information, level, pValue, testingStat);

            return information;
        }

        public static HypothesisTestingInformation VarianceTesting(IEnumerable<double> data1, double level = 0.95,double nullVariance = 1, bool? isRightWay = null)
        {
            HypothesisTestingInformation information = new HypothesisTestingInformation();
            
            var basicStat = new BasicStatistics(data1);
            var n = basicStat.Count;
            var variance = basicStat.Variance;
            var chiSquare = new ChiSquare(n - 1);
            var testStat = (n - 1)*variance/nullVariance;
            var pValue=GetPValue(nullVariance,testStat, chiSquare, isRightWay);

            GetInformation(information,level,pValue,testStat);
            return information;
        }

        public static HypothesisTestingInformation VarianceTesting(IEnumerable<double> data1, IEnumerable<double> data2, double level = 0.95, double nullVariance = 1, bool? isRightWay = null)
        {
            HypothesisTestingInformation information = new HypothesisTestingInformation();
          
            var basicStat1 = new BasicStatistics(data1);
            var basicStat2 = new BasicStatistics(data2);
            var n1 = basicStat1.Count;
            var n2 = basicStat2.Count;
            var variance1 = basicStat1.Variance;
            var variance2 = basicStat2.Variance;

            var testStat = variance1/variance2;
            
            
            
            var fDistribution = new FisherSnedecor(n1 - 1,n2-1);
            
            var pValue = GetPValue(nullVariance,testStat, fDistribution, isRightWay);

            GetInformation(information, level, pValue, testStat);
            return information;
        }
    
    
    }
    public class HypothesisTestingInformation
    {
        public double Level { get; set; }
        public double PValue { get; set; }
        public bool IsRejectH0 { get; set; }
        public double TestStatistics { get; set; }
    }
}
