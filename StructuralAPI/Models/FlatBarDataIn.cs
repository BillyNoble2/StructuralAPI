namespace StructuralAPI.Models
{
    public class FlatBarDataIn
    {
        public double TensionLoad { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double BoltDiameter { get; set; }
        public string PlateGrade { get; set; }
        public double BoltGrade { get; set; }
    }
}
