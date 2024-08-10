namespace StructuralAPI.Models
{
    public class GablePostDataIn
    {
        public double ColumnHeight { get; set; }
        public double ColumnCentres { get; set; }
        public double FrameCentres { get; set; }
        public double PositiveWindPressure { get; set; }
        public double NegativeWindPressure { get; set; }
        public double DeadLoadFactor { get; set; }
        public double LiveLoadFactor { get; set; }
        public double WindLoadFactor { get; set; }
        public double RoofDeadLoad {  get; set; }
        public double RoofLiveLoad { get; set; }
        public double AllowableDeflection { get; set; }

    }
}
