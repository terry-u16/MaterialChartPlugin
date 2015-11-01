using System;
using System.Collections.Generic;
using System.Linq;
using MaterialChartPlugin.Models;
using MaterialChartPlugin.Models.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaterialChartPluginTest
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void DistinctTest()
        {
            var source1 = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 0, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 1, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 3, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 4, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            var source2 = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 5, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 6, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 7, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 8, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 9, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            var merged = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 0, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 1, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 3, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 4, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 5, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 6, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 7, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 8, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 9, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            source1.Concat(source2).Distinct(p => p.DateTime).OrderBy(p => p.DateTime).Is(merged);

            source2 = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 3, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 4, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 5, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 6, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 7, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 8, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 9, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            source1.Concat(source2).Distinct(p => p.DateTime).OrderBy(p => p.DateTime).Is(merged);

            source2 = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 300, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 3, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 4, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 5, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 6, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 7, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 8, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 9, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            source1.Concat(source2).Distinct(p => p.DateTime).OrderBy(p => p.DateTime).Is(merged);
        }

        [TestMethod]
        public void UnionTest()
        {
            var source1 = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 0, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 1, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 3, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 4, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            var source2 = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 5, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 6, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 7, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 8, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 9, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            var merged = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 0, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 1, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 3, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 4, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 5, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 6, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 7, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 8, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 9, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            source1.Union(source2, p => p.DateTime).OrderBy(p => p.DateTime).Is(merged);

            source2 = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 3, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 4, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 5, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 6, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 7, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 8, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 9, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            source1.Union(source2, p => p.DateTime).OrderBy(p => p.DateTime).Is(merged);

            source2 = new List<TimeMaterialsPair>
            {
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 2, 0, 0), 300, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 3, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 4, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 5, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 6, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 7, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 8, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30),
                new TimeMaterialsPair(new DateTime(2015, 11, 1, 9, 0, 0), 100, 200, 300, 400, 500, 10, 20, 30)
            };

            source1.Union(source2, p => p.DateTime).OrderBy(p => p.DateTime).Is(merged);
        }
    }
}
