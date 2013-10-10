using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;

namespace StatisticsMethods
{

    class MathDotNetAdditional
    {
        //微分
        private double Differential(IContinuousDistribution continuousDistribution,double x)
        {
            var delta = Math.Pow(10, -8);
            var rightX = x + delta;
            var leftX = x - delta;
            var result = (continuousDistribution.Density(rightX) - continuousDistribution.Density(leftX))/
                         (rightX - leftX);
            return result;

        }
        
        public static double Dichotomy(IDistribution disbution, double goalValue)
        {
            return Dichotomy(disbution, goalValue, 0, 1);
        }

        //兩分法
        public static double Dichotomy(IDistribution disbution,double goalValue,double lower,double upper)
        {
            
            var upperCdf = disbution.CumulativeDistribution(upper);
            var lowerCdf = disbution.CumulativeDistribution(lower);
            //bool isInInterval = (goalValue < upperCdf) & (goalValue > lowerCdf);
            bool isOver = goalValue > upperCdf;
            bool isUnder = goalValue < lowerCdf;
            while (isOver|isUnder)
            {
                if (isOver)
                {
                    upper += 100;
                    upperCdf = disbution.CumulativeDistribution(upper);
                }
                if (isUnder)
                {
                    lower -= 100;
                    lowerCdf = disbution.CumulativeDistribution(lower);
                }
                isOver = goalValue > upperCdf;
                isUnder = goalValue < lowerCdf;
                
            }
            
            double half = (upper + lower) / 2d;
 
            while (Math.Abs(upperCdf - goalValue) > Math.Pow(10, -5))
            {
               // lowerCdf = disbution.CumulativeDistribution(lower);
                var halfCdf = disbution.CumulativeDistribution(half);
                
                if (halfCdf < goalValue)
                    lower = half;
                if (halfCdf > goalValue)
                    upper = half;
                half = (upper + lower) / 2d;
                upperCdf = disbution.CumulativeDistribution(upper);
            }
            return half;

        }
    }
}
