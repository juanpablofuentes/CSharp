using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Group.Salto.Testing.HelpersTesting
{
    [TestClass]
    public class DateRangeValidation
    {
        private List<PeopleCost> peopleCosts = null;

        public DateRangeValidation()
        {
            peopleCosts = new List<PeopleCost>()
            {
                new PeopleCost(){HourCost = Convert.ToDecimal(2.50), StartDate = new DateTime(2018, 3, 12), EndDate = new DateTime(2018, 7, 12) },
                new PeopleCost(){HourCost = Convert.ToDecimal(8.29), StartDate = new DateTime(2018, 11, 2), EndDate = new DateTime(2018, 12, 20) },
                new PeopleCost(){HourCost = Convert.ToDecimal(15.00), StartDate = new DateTime(2018, 12, 25), EndDate = new DateTime(2019, 01, 31) }
            };
        }

        [TestMethod]
        public void DataRangeCreateCorrect()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = 125,
                StartDate = new DateTime(2018, 08, 01),
                EndDate = new DateTime(2018, 08, 31),
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DataRangeCreateCorrectWithOutEnd()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = 125,
                StartDate = new DateTime(2019, 05, 01)
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DataRangeCreateInvalid()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = 125,
                StartDate = new DateTime(2018, 03, 14),
                EndDate = new DateTime(2018, 03, 16),
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DataRangeCreateInvalid2()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = 125,
                StartDate = new DateTime(2018, 03, 14),
                EndDate = new DateTime(2018, 12, 31),
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DataRangeCreateInvalid3()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = 125,
                StartDate = new DateTime(2017, 01, 01),
                EndDate = new DateTime(2019, 12, 31),
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DataRangeCreateInvalid4()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = 125,
                StartDate = new DateTime(2017, 01, 01),
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DataRangeCreateInvalid5()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = 125,
                StartDate = new DateTime(2018, 06, 01),
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DataRangeEditValid()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = Convert.ToDecimal(8.29),
                StartDate = new DateTime(2018, 11, 15),
                EndDate = new DateTime(2018, 12, 10),
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DataRangeEditInValid()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = Convert.ToDecimal(8.29),
                StartDate = new DateTime(2018, 12, 23),
                EndDate = new DateTime(2019, 1, 15),
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DataRangeEditInValid2()
        {
            PeopleCostDetailDto peopleCostDetailDto = new PeopleCostDetailDto()
            {
                HourCost = Convert.ToDecimal(8.29),
                StartDate = new DateTime(2018, 7, 1),
                EndDate = new DateTime(2018, 12, 1),
            };

            bool result = ExistCostsInSameDates(peopleCostDetailDto, peopleCosts);
            Assert.IsTrue(result);
        }

        private bool ExistCostsInSameDates(PeopleCostDetailDto peopleCost, List<PeopleCost> peopleCosts)
        {
            PeopleCost costsInDdates = null;

            if (!peopleCost.EndDate.HasValue)
            {
                costsInDdates = peopleCosts.Where(pc => (peopleCost.StartDate >= pc.StartDate && peopleCost.StartDate <= pc.EndDate) || (pc.StartDate >= peopleCost.StartDate)).FirstOrDefault();
            }
            else
            {
                costsInDdates = peopleCosts.Where(pc => (peopleCost.StartDate >= pc.StartDate && peopleCost.StartDate <= pc.EndDate)
                || (peopleCost.EndDate >= pc.StartDate && peopleCost.EndDate <= pc.EndDate)
                || (pc.StartDate >= peopleCost.StartDate && pc.StartDate <= peopleCost.EndDate)
                || (pc.EndDate >= peopleCost.StartDate && pc.EndDate <= peopleCost.EndDate)
                || (pc.EndDate == null && pc.StartDate <= peopleCost.StartDate)).FirstOrDefault();
            }

            if (costsInDdates != null)
            {
                if (peopleCost.HourCost.HasValue && costsInDdates.HourCost == peopleCost.HourCost) return false;
                return true;
            }

            return false;
        }
    }

    public class PeopleCostDetailDto
    {
        public decimal? HourCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class PeopleCost 
    {
        public decimal? HourCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
