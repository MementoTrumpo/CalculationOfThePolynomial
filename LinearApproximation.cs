using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationOfThePolynomial
{
    public class LeastSquaresMethod
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double X_2 { get; set; }

        public double X_3 { get; set; }
        
        public double X_4 { get; set; }
    
        public double XY { get; set; }
    
        public double X_2Y { get; set; }
     

        public  LeastSquaresMethod(double x, double y)
        {
            X = x;
            Y = y;
            X_2 = x * x;
            X_3 = X_2 * x;
            X_4 = X_3 * x;
            XY = x * y;
            X_2Y = X_2 * y;
        }
       
    }
}
