using System;
using NUnit.Framework;
using Expedia;
using System.Collections.Generic;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatCarDoesGetLocationFromTheDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            String car = "Beetle";
            String truck = "Wat";

            using (mocks.Record())
            {
                mockDatabase.getCarLocation(1);
                LastCall.Return(car);

                mockDatabase.getCarLocation(2);
                LastCall.Return(truck);
            }

            var target = new Car(10);
            target.Database = mockDatabase;

            String result = target.getCarLocation(1);
            Assert.AreEqual(result, car);

            result = target.getCarLocation(2);
            Assert.AreEqual(result, truck);
        }

        [Test()]
        public void TestThatCarDoesGetMileageFromDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            int Mileage = 100;
            mockDatabase.Miles = Mileage;
            var target = ObjectMother.BMW();

            target.Database = mockDatabase;

            int mileCount = target.Mileage;
            Assert.AreEqual(mileCount, Mileage);
        }

        [Test()]
        public void TestThatObjectMotherWorksProperly()
        {
            var target = ObjectMother.BMW();
            Assert.AreEqual("It's a BMW.", target.Name);
        }

	}
}
