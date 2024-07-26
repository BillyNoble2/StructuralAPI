using static StructuralAPI.Controllers.FlatBarController;
using StructuralAPI.Models;
using System;

namespace StructuralAPI.CalculationEngines
{
    public class FlatBarDesigner
    {
        public static double Calculator(FlatBarData request)
        {
            double grossArea = CalculateGrossArea(request.Width, request.Thickness);
            double boltHoleDiameter = DetermineBoltHoleDia(request.BoltDiameter);
            double numberOfBolts = CalculateNumberBolts(request.BoltDiameter, request.TensionLoad);
            double boltsPerRow = DetermineBoltsInRow(request.BoltDiameter, numberOfBolts, request.Width);
            return grossArea;
        }

        /// <summary>
        /// Calculate gross area of flatbar
        /// </summary>
        /// <param name="width">mm</param>
        /// <param name="thickness">mm</param>
        /// <returns>area in mm^2</returns>
        public static double CalculateGrossArea(double width, double thickness)
        {
            return width * thickness;
        }
        public static double DetermineBoltHoleDia(double boltDia)
        {
            switch (boltDia)
            {
                case 20:
                    return 22;
                case 24:
                    return 26;
                case 30:
                    return 33;
                default:
                    throw new ArgumentException("Invalid bolt diameter");
            }
        }
        public static double CalculateNumberBolts(double boltDia, double tensionLoad)
        {
            double boltShearRes = 0;
            switch (boltDia)
            {
                case 20:
                    boltShearRes = 94.1;
                    break;
                case 24:
                    boltShearRes = 136;
                    break;
                case 30:
                    boltShearRes = 215;
                    break;
                default:
                    throw new ArgumentException("Unable to calculate bolt shear resistance");
            }
            double numberBolts = tensionLoad / boltShearRes;
            numberBolts = Math.Ceiling(numberBolts);

            if(numberBolts % 2 != 0 && numberBolts % 3 != 0)
            {
                numberBolts = numberBolts + 1;
            }

            return numberBolts; 
        }
        public static double DetermineBoltsInRow(double boltDia, double numberBolts, double plateWidth)
        {
            switch (boltDia)
            {
                case 20:
                    if (plateWidth >= 180 && numberBolts % 3 == 0)
                    {
                        return 3;
                    }
                    if (plateWidth >= 180 && numberBolts % 2 == 0)
                    {
                        return 2;

                    }
                    if (plateWidth < 180 && numberBolts % 2 == 0)
                    {
                        return 2;

                    }
                    break;
                case 24:
                    if (plateWidth >= 210 && numberBolts % 3 == 0)
                    {
                        return 3;
                    }
                    if (plateWidth >= 210 && numberBolts % 2 == 0)
                    {
                        return 2;
                    }
                    if (plateWidth < 210 && numberBolts % 2 == 0)
                    {
                        return 2;
                    }
                    break;
                case 30:
                    if (plateWidth >= 300 && numberBolts % 3 == 0)
                    {
                        return 3;
                    }
                    if (plateWidth >= 300 && numberBolts % 2 == 0)
                    {
                        return 2;
                    }
                    if (plateWidth < 300 && numberBolts % 2 == 0)
                    {
                        return 2;
                    }
                    break;
                default:
                    throw new ArgumentException("Unable to determine number of botls per row");
            }
            throw new ArgumentException("Unable to determine number of botls per row");
        }
    }
}
