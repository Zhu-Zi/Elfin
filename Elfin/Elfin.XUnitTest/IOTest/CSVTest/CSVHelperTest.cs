using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Elfin.IO.CSV;

namespace Elfin.XUnitTest.IOTest.CSVTest
{
    public class CSVHelperTest
    {
        [Fact]
        public void ReadCSVFile_Test()
        {
            var filePath = @".\IOTest\CSVTest\SampleFiles\Kline_1h_201808_btc_usdt.csv";
            var dataList = CSVHelper.ReadCSVFile(filePath);
            var count = dataList.Count;

            if (count > 0)
            {
                Assert.True(true);
            }
            else
            {
                Assert.False(true);
            }
        }
    }
}
