using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Elfin.IO.CSV;
using Elfin.XUnitTest.IOTest.CSVTest.Moldes;

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

        [Fact]
        public void WriteCSVFile_Test()
        {
            var infoList = new List<CSVHelperModel>();
            infoList.Add(new CSVHelperModel { Name = "韩子昂", GraduatedSchool = "上海大学", Company = "联合政府交通运输部", Remarks = "《流浪地球》" });
            infoList.Add(new CSVHelperModel { Name = "韩朵朵", GraduatedSchool = "北京大学", Company = "联合政府军队", Remarks = "《流浪地球》" });
            infoList.Add(new CSVHelperModel { Name = "刘长条", GraduatedSchool = "清华大学", Company = "联合政府紧急技术观察员", Remarks = "《流浪地球》" });
            infoList.Add(new CSVHelperModel { Name = "逻辑", GraduatedSchool = "MIT", Company = "联合国", Remarks = "《三体》" });
            infoList.Add(new CSVHelperModel { Name = "维德", GraduatedSchool = "西点军校", Company = "圣母的公司", Remarks = "《三体》" });
            infoList.Add(new CSVHelperModel { Name = "云天明", GraduatedSchool = "北京航空航天大学", Company = "联合国", Remarks = "《三体》" });
            infoList.Add(new CSVHelperModel { Name = "章北海", GraduatedSchool = "空军指挥学院", Company = "Ministry of National Defense", Remarks = "《三体》" });
            infoList.Add(new CSVHelperModel { Name = "艾AA", GraduatedSchool = "MIT", Company = "圣母的公司", Remarks = "《三体》" });

            var folderPath = @".\IOTest\CSVTest\SampleFiles\";
            var fileName = "Test";
            var isTrue = CSVHelper.WriteCSVFile(folderPath, fileName, infoList);

            if (isTrue)
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
