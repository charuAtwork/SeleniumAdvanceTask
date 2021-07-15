using System;
using System.Collections.Generic;
using System.Text;
using SkillSharePortal_ServiceList.Config;
using SkillSharePortal_ServiceList.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
//using RelevantCodes.ExtentReports;
//using ExtentReports;
using AventStack.ExtentReports;

namespace SkillSharePortal_ServiceList.Global
{
  public class Base_bck 
  {
        /*#region To access Path from resource file
         public static int Browser = Int32.Parse(SkillShareResource.WebBrowser);
         public static String ExcelPath = SkillShareResource.ExcelPath;
         public static string ScreenshotPath = SkillShareResource.ScreenShotPath;
         public static string ReportPath = SkillShareResource.ReportPath;
        #endregion*/

        #region reports
          public static ExtentTest test;
          public static ExtentReports extent;
          protected static object driver;

        #endregion

        #region setup and tear down
        public void Initialize()
            {

              /*  #region Initialise Reports

                extent = new ExtentReports(ReportPath, false, DisplayOrder.NewestFirst);
                extent.LoadConfig(SkillShareResource.ReportXMLPath);

                #endregion*/
                
            /*    if (SkillShareResource.IsLogin == "true")
                {
                     loginObj = new ();
                    loginObj.LogInPortal();
                }
                else
                {
                   RegisterNewUser ObjNewUser = new RegisterNewUser();
                   ObjNewUser.NewUser();
                }*/

            }


           // [TearDown]
            public void TearDown()
            {
                // Screenshot
             //   String img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Report");//AddScreenCapture(@"E:\Dropbox\VisualStudio\Projects\Beehive\TestReports\ScreenShots\");
             //   test.Log(LogStatus.Info, "Image example: " + img);
                // end test. (Reports)
               //   extent.EndTest(test);
             // calling Flush writes everything to the log file (Reports)
               // extent.Flush();
                
                // Close the driver 
                //GlobalDefinitions.driver.Close();
                GlobalDefinitions.driver.Quit();
            }
            #endregion

        }
    }

