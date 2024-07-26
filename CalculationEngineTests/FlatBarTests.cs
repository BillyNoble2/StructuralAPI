using StructuralAPI.CalculationEngines;

namespace CalculationEngineTests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {

            //SHOULD SPOOF A REQUEST
            //Arrange
            double width = 200;
            double thickness = 20;

            //Act
            double actualResult = StructuralAPI.CalculationEngines.FlatBarDesigner.CalculateGrossArea(width, thickness);
            double expectedResult = 4000;

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}