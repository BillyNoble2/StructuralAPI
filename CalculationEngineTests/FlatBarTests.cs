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

            FlatBarDataOut expectedResult = new FlatBarDataOut{
                
            };

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}