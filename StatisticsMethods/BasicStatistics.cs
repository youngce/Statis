using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace StatisticsMethods
{
    public class BasicStatistics
    {
        private static  DescriptiveStatistics _statistics ;

        public int Count;
        public int RowCount;
        public int ColumeCount;
        
        public double Maximum;
        public double Minimum;
        public double Median;
        
        public double Mean;
        public double Variance;
        public double StandardDeviation;
        
        public double Kurtosis;
        public double Skewness;

        //public double? RowMeans;
        //public double? ColumeMeans;
        //public double? RowVariances;
        //public double? ColumeVariances;
        //public double? RowStandardDeviations;
        //public double? ColumeStandardDeviations;

        


        #region 建構函數
        public BasicStatistics(IEnumerable<double> data)
        {
            _statistics = new DescriptiveStatistics(data);
            GetStatistics(_statistics);

        }
        public BasicStatistics(IEnumerable<double?> data, bool increasedAccuracy)
        {
            _statistics = new DescriptiveStatistics(data, increasedAccuracy);
            GetStatistics(_statistics);
        }
        public BasicStatistics(IEnumerable<double> data, bool increasedAccuracy)
        {
            _statistics = new DescriptiveStatistics(data, increasedAccuracy);
            GetStatistics(_statistics);
        }
        #endregion

        private void GetStatistics(DescriptiveStatistics statistics)
        {
            Count = statistics.Count;

            Maximum=statistics.Maximum;
            Minimum = statistics.Minimum;
            Median = statistics.Median;

            Mean = statistics.Mean;
            Variance = statistics.Variance;
            StandardDeviation = statistics.StandardDeviation;

            Kurtosis = statistics.Kurtosis;
            Skewness = statistics.Skewness;


        }

      
    }
}
