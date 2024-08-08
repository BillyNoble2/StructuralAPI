using StructuralAPI.Models;

namespace StructuralAPI.CalculationEngines
{
    public class GablePostDesigner
    {
        public static void GablePostCalculator(GablePostDataIn request)
        {
            double windUDL = CalculateUniformWindLoad(request.PositiveWindPressure, request.ColumnCentres, request.WindLoadFactor);
            double factoredAxialLoad = CalculateAxialLoad(request.DeadLoadFactor, request.LiveLoadFactor, request.RoofDeadLoad, request.RoofLiveLoad, request.ColumnCentres, request.FrameCentres);
            double factoredBendingMoment = CalculateBendingMoment(windUDL, request.WindLoadFactor, request.ColumnHeight);
            double factoredShearForce = CalculateShearForce(windUDL, request.WindLoadFactor, request.ColumnHeight);
            double maximumAllowableDef = CalculateMaxAllowableDefl(request.ColumnHeight, request.AllowableDeflection);
        }
        /// <summary>
        /// Calculate uniform wind load on post (Factored).
        /// </summary>
        /// <param name="posWindPress"></param>
        /// <param name="columnHeight"></param>
        /// <param name="columnCentres"></param>
        /// <returns>UDL (Factored)</returns>
        public static double CalculateUniformWindLoad(double posWindPress, double columnCentres, double wLFactor)
        {
            return (posWindPress * columnCentres);
        }
        /// <summary>
        /// Calculate axial load in post (Factored)
        /// </summary>
        /// <param name="dLFactor"></param>
        /// <param name="lLFactor"></param>
        /// <param name="roofDeadLoad"></param>
        /// <param name="roofLiveLoad"></param>
        /// <param name="columnCentres"></param>
        /// <param name="frameCentres"></param>
        /// <returns></returns>
        public static double CalculateAxialLoad(double dLFactor, double lLFactor, double roofDeadLoad, double roofLiveLoad, double columnCentres, double frameCentres)
        {
            var halfFrameCentre = frameCentres / 2;
            var roofArea = halfFrameCentre * columnCentres;
            var factoredAreaLoad = (dLFactor * roofDeadLoad) + (lLFactor * roofLiveLoad);

            return (factoredAreaLoad * roofArea);
        }
        /// <summary>
        /// Calculates bending moment due to wind load (Factored)
        /// </summary>
        /// <param name="windUDL"></param>
        /// <param name="wLFactor"></param>
        /// <param name="columnHeight"></param>
        /// <returns>Factored bending moment</returns>
        public static double CalculateBendingMoment(double windUDL, double wLFactor, double columnHeight)
        {
            return ((windUDL * (columnHeight*columnHeight))/8)*wLFactor;
        }
        /// <summary>
        /// Calculates shear force due to wind load (Factored)
        /// </summary>
        /// <param name="windUDL"></param>
        /// <param name="wLFactor"></param>
        /// <param name="columnHeight"></param>
        /// <returns>Factored shear force</returns>
        public static double CalculateShearForce(double windUDL, double wLFactor, double columnHeight)
        {
            return ((windUDL * columnHeight) / 2) * windUDL;
        }
        /// <summary>
        /// Maximum allowable deflection value (mm).
        /// </summary>
        /// <param name="columnHeight"></param>
        /// <param name="defLimVal"></param>
        /// <returns></returns>
        public static double CalculateMaxAllowableDefl(double columnHeight, double defLimVal)
        {
            return (columnHeight / defLimVal);
        }

    }
}
