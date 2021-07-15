using ExcelDataReader;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using SkillSharePortal_ServiceList.Config;
using System.Threading.Tasks;
//using RelevantCodes.ExtentReports;
//using ExtentReports;
using AventStack.ExtentReports;
using SeleniumExtras.WaitHelpers;

namespace SkillSharePortal_ServiceList.Global
{
    public class GlobalDefinitions
    {
        public static IWebDriver driver { get; set; }

        #region To access Path from resource file
        public static string filepath = "\\Data\\Data.xlsx";
        public static string ExcelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + filepath);
        public static int Browser = Int32.Parse(SkillShareResource.WebBrowser);
        public static string ScreenshotPath = "C:\\Users\\charulamba\\IndConn\\ADVANC TASK\\FromCharu\\SkillSharePortal_ServiceList\\ScreenShots\\";
        //public static string ScreenShotsPath = "\\ScreenShots";
        //public static string ScreenshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + ScreenShotsPath);
        //public static String ExcelPath = SkillShareResource.ExcelPath;
        //public static string ScreenshotPath = SkillShareResource.ScreenShotPath;
        //public static string ReportPath = SkillShareResource.ReportPath;
          #endregion

        #region reports
        public static ExtentTest test;
        //public static ExtentReports extent;
        #endregion

        #region Wait
        public static void ImplicitWait(int time)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        }
        public static IWebElement WaitForElement(IWebDriver driver, By by, int timeOutinSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutinSeconds));
            return (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by)));
        }

        /*public static IWebElement ElementPresent(IWebDriver driver, By by, int timeOutinSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutinSeconds));
            return (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by)));
        }*/

        
        public static void ElementIsVisible(IWebDriver driver, string locator, string locatorvalue)
        {
            try
            {
                if (locator == "Id")
                {
                    var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(locatorvalue)));
                }
                if (locator == "XPath")
                {
                    var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(locatorvalue)));

                }
                if (locator == "CssSelector")
                {
                    var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(locatorvalue)));

                }
            }
           catch (Exception ex)
            {
                Assert.Fail("Test failed while waiting for a webelement", ex.Message);
            }

        }

        #endregion

        
        
        #region ExcelForTestData
        public class ExcelLib
        {
            private static readonly List<Datacollection> DataCol = new List<Datacollection>();
            // The following code helps to quit the windows in which you only need to pass the name of excel.
            // ReSharper disable once UnusedMember.Local
            private static void QuitExcel(string processtitle)
            {
                var processes = from p in Process.GetProcessesByName("EXCEL")
                                select p;
                foreach (var process in processes)
                    if (process.MainWindowTitle == "Microsoft Excel - " + processtitle + " - Excel")
                        process.Kill();
            }
            private static void ClearData()
            {
                DataCol.Clear();
            }
            private static DataTable ExcelToDataTable(string fileName, string sheetName)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                // Open file and return as Stream
                using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        //Get all the tables
                        var table = result.Tables;
                        // store it in data table
                        var resultTable = table[sheetName];
                        return resultTable;
                    }
                }
            }

            public static string ReadData(int rowNumber, string columnName)
            {
                try
                {
                    //Retriving Data using LINQ to reduce much of iterations
                    rowNumber = rowNumber - 1;
                    var data = (from colData in DataCol
                                where (colData.ColName == columnName) && (colData.RowNumber == rowNumber)
                                select colData.ColValue).SingleOrDefault();
                    //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                    return data;
                }
                catch (Exception e)
                {
                    // ReSharper disable once LocalizableElement
                    Console.WriteLine("Exception occurred in ExcelLib Class ReadData Method!" + Environment.NewLine +
                                      e.Message);
                    return null;
                }
            }
            public static void PopulateInCollection(string fileName, string sheetName)
            {
                ClearData();
                var table = ExcelToDataTable(fileName, sheetName);
                //Iterate through the rows and columns of the Table
                for (var row = 1; row <= table.Rows.Count; row++)
                    for (var col = 0; col < table.Columns.Count; col++)
                    {
                        var dtTable = new Datacollection
                        {
                            RowNumber = row,
                            ColName = table.Columns[col].ColumnName,
                            ColValue = table.Rows[row - 1][col].ToString()
                        };
                        //Add all the details for each row
                        DataCol.Add(dtTable);
                    }
            }
            private class Datacollection
            {
                public int RowNumber { get; set; }
                public string ColName { get; set; }
                public string ColValue { get; set; }
            }
        }
    }



    #endregion

    #region ScreenShot
    public class SaveScreenShotClass
        {
            public static string SaveScreenshot(IWebDriver driver, string ScreenShotFileName) // Definition
            {
                var folderLocation = GlobalDefinitions.ScreenshotPath;
             
                if (!System.IO.Directory.Exists(folderLocation))
                {
                    System.IO.Directory.CreateDirectory(folderLocation);
                }

                var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
                var fileName = new StringBuilder(folderLocation);

                fileName.Append(ScreenShotFileName);
                fileName.Append(DateTime.Now.ToString("_dd-mm-yyyy_mss"));
            
            //fileName.Append(DateTime.Now.ToString("dd-mm-yyyym_ss"));
            //fileName.Append(".jpeg"); 
            //screenShot.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Jpeg);
            fileName.Append(".Png");
            screenShot.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Png);
            return fileName.ToString();
            }
        }
        #endregion
}

