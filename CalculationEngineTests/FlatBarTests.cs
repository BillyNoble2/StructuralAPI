using StructuralAPI.CalculationEngines;
using StructuralAPI.Models;

namespace CalculationEngineTests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            //Arrange
            FlatBarDataIn flatBar = new FlatBarDataIn();
            flatBar.TensionLoad = 200;
            flatBar.Width = 200;
            flatBar.Thickness = 20;
            flatBar.BoltDiameter = 20;
            flatBar.PlateGrade = "355";
            flatBar.BoltGrade = 8.8;


            //Act
            FlatBarDataOut actualResult = StructuralAPI.CalculationEngines.FlatBarDesigner.Calculator(flatBar);

            FlatBarDataOut expectedResult = new FlatBarDataOut
            {
                GrossArea = 4000,
                BoltHoleDiameter = 22,
                NumberOfBolts = 3,
                BoltsPerRow = 3,
                NetArea = 2680,
                TotalBoltShearRes = 282.3,
                PlateFu = 490,
                TensionResistance = 1074.4,
                DimA = 30,
                DimB = 70,
                DimC = 40,
                DimD = 60,
                Diagram = "B"
            };

            //Assert
            Assert.AreEqual(expectedResult.GrossArea, actualResult.GrossArea);
            Assert.AreEqual(expectedResult.BoltHoleDiameter, actualResult.BoltHoleDiameter);
            Assert.AreEqual(expectedResult.NumberOfBolts, actualResult.NumberOfBolts);
            Assert.AreEqual(expectedResult.BoltsPerRow, actualResult.BoltsPerRow);
            Assert.AreEqual(expectedResult.NetArea, actualResult.NetArea);
            Assert.AreEqual(expectedResult.TotalBoltShearRes, actualResult.TotalBoltShearRes);
            Assert.AreEqual(expectedResult.PlateFu, actualResult.PlateFu);
            Assert.AreEqual(expectedResult.TensionResistance, actualResult.TensionResistance);
            Assert.AreEqual(expectedResult.DimA, actualResult.DimA);
            Assert.AreEqual(expectedResult.DimB, actualResult.DimB);
            Assert.AreEqual(expectedResult.DimC, actualResult.DimC);
            Assert.AreEqual(expectedResult.DimD, actualResult.DimD);
            Assert.AreEqual(expectedResult.Diagram, actualResult.Diagram);
        }
    }
}