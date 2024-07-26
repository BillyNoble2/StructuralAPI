using static StructuralAPI.Controllers.FlatBarController;
using StructuralAPI.Models;
using System;
using System.Reflection.Metadata;

namespace StructuralAPI.CalculationEngines
{
    public class FlatBarDesigner
    {
        public static FlatBarDataOut Calculator(FlatBarDataIn request)
        {
            double grossArea = CalculateGrossArea(request.Width, request.Thickness);
            double boltHoleDiameter = DetermineBoltHoleDia(request.BoltDiameter);
            double numberOfBolts = CalculateNumberBolts(request.BoltDiameter, request.TensionLoad);
            double boltsPerRow = DetermineBoltsInRow(request.BoltDiameter, numberOfBolts, request.Width);
            double netArea = CalculateNetArea(request.Width, request.Thickness, boltsPerRow, boltHoleDiameter);
            double totalBoltShearRes = CalculateBoltShearRes(request.BoltDiameter, numberOfBolts);
            double plateFu = DeterminePlateFu(request.PlateGrade, request.Thickness);
            double tentionResistance = CalculateTensionResistance(netArea, plateFu);

            double dimA = CalculateDimA(request.BoltDiameter);
            double dimB = CalculateDimB(request.BoltDiameter, boltsPerRow, request.Width);
            double dimC = CalculateDimC(request.BoltDiameter);
            double dimD = CalculateDimD(boltsPerRow, numberOfBolts, request.BoltDiameter);

            var response = new FlatBarDataOut
            {
                GrossArea = grossArea,
                BoltHoleDiameter = boltHoleDiameter,
                NumberOfBolts = numberOfBolts,
                BoltsPerRow = boltsPerRow,
                NetArea = netArea,
                TotalBoltShearRes = totalBoltShearRes,
                DimA = dimA,
                DimB = dimB,
                DimC = dimC,
                DimD = dimD
            };
            return response;
        }

        #region StructuralCalculations

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
        public static double CalculateNetArea(double width, double thickness, double boltsPerRow, double boltHoleDiameter)
        {
            return (width * thickness) - (boltsPerRow * boltHoleDiameter * thickness);
        }
        public static double CalculateBoltShearRes(double boltDiameter, double numberOfBolts)
        {
            double boltShearRes = 0;
            switch (boltDiameter)
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
                    throw new ArgumentException("Unable to calculate total bolt shear resistance.");
            }
            var totalShearRes = (boltShearRes * numberOfBolts);
            return Math.Round(totalShearRes,1);
        }
        public static double DeterminePlateFu(string plateGrade, double thickness)
        {
            switch (plateGrade)
            {
                case "355":
                    if (thickness <= 40)
                    {
                        return 490;
                    }
                    if (thickness > 40 && thickness <= 80)
                    {
                        return 470;
                    }
                    else
                    {
                        throw new ArgumentException("Not workable plate thickness");
                    }
                case "275":
                    if (thickness <= 40)
                    {
                        return 430;
                    }
                    if (thickness > 40 && thickness <= 80)
                    {
                        return 410;
                    }
                    else
                    {
                        throw new ArgumentException("Not workable plate thickness");
                    }
                default:
                    throw new ArgumentException("Not workable plate thickness");
            }
        }
        public static double CalculateTensionResistance(double netArea, double plateFu)
        {
            //BS EN 1993-1-1 CL 6.2.3
            var gammaM2 = 1.1;
            double resistance = ((0.9 * netArea * plateFu) / gammaM2) / 1000;
            resistance = Math.Round(resistance, 1);
            return resistance;
        }

        #endregion

        #region GeometryCalculations

        public static double CalculateDimA(double boltDiameter)
        {
            switch (boltDiameter)
            {
                case 20:
                    return 30;
                case 24:
                    return 35;
                case 30:
                    return 45;
                default:
                    throw new ArgumentException("Unable to calculate dim A");
            }
        }
        public static double CalculateDimB(double boltDiameter, double boltsPerRow, double plateWidth)
        {
            double minEdgeDist = 0;
            double minBoltCC = 0;

            switch (boltDiameter)
            {
                case 20:
                    minEdgeDist = 30;
                    minBoltCC = 60;
                    break;
                case 24:
                    minEdgeDist = 35;
                    minBoltCC = 70;
                    break;
                case 30:
                    minEdgeDist = 45;
                    minBoltCC = 90;
                    break;
                default:
                    throw new ArgumentException("Unable to calculate dim B");
            }

            double availableBoltWidth = plateWidth - (2 * minEdgeDist);
            double boltSpaces;
            if (boltsPerRow > 1)
            {
                boltSpaces = boltsPerRow - 1;
            }
            else
            {
                boltSpaces = boltsPerRow;
            }

            double boltCC = availableBoltWidth / boltSpaces;
            return boltCC;
        }
        public static double CalculateDimC(double boltDiameter)
        {
            switch (boltDiameter)
            {
                case 20:
                    return 40;
                case 24:
                    return 50;
                case 30:
                    return 60;
                default:
                    throw new ArgumentException("Unable to calculate dim C");
            }
        }
        public static double CalculateDimD(double boltsPerRow, double numberOfBolts, double boltDiameter)
        {
            var multipleRows = false;
            if (numberOfBolts > boltsPerRow)
            {
                multipleRows = true;
            }
            else
            {
                multipleRows = false;
            }

            if (multipleRows = true)
            {
                switch (boltDiameter)
                {
                    case 20:
                        return 60;
                    case 24:
                        return 70;
                    case 30:
                        return 90;
                    default:
                        throw new ArgumentException("Unable to calculate Dim D");
                }
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
