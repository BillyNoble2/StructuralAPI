using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructuralAPI.CalculationEngines;
using StructuralAPI.Models;

namespace CalculationEngineTests
{
    internal class GablePostTest
    {
        [Test]
        public void TestInertia()
        {
            //Arrange
            var windUDL = 3;
            var columnHeight = 10;
            var deflectionLimit = 30;

            //Act
            var actualResult = GablePostDesigner.CalculateMinimumInertia(windUDL, columnHeight, deflectionLimit);
            var expectedResult = 6351;

            //Assert
            Assert.AreEqual(expectedResult, actualResult, 1);
        }

    }
}
