using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SeleniumExtras.PageObjects;
using SkillSharePortal_ServiceList.Global;
using SkillSharePortal_ServiceList.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkillSharePortal_ServiceList.Tests
{
    [TestFixture]

    [Description("Notification")]
    class NotificationTestSuite : GlobalDefinitions
    {
       
        //for ExtentTest
        public static ExtentTest test;

        //for Extent Reports
        public static ExtentReports extent;

        [SetUp]
        public void Setup()
        {
            switch (Browser)
            {
                case 1:
                    GlobalDefinitions.driver = new ChromeDriver();
                    break;

                case 2:
                    GlobalDefinitions.driver = new FirefoxDriver();
                    break;
            }
            GlobalDefinitions.driver.Manage().Window.Maximize();
        }

        [OneTimeSetUp]

        public void StartReport()
        {
            extent = new ExtentReports();

            //To obtain the current solution path/project path
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\TestRunReport.html";

            var htmlReporter = new ExtentHtmlReporter(reportPath);

            //Add QA system info to html report
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "LocalHost");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Charu");
        }
    
        [Test, Description("Test Case: Notification Message for Request to Seller"), Order(1)]
        public void NotificationMsg()
        {
            test = extent.CreateTest("Notification Request to Seller").Info("Verifying,Notification Message for Request to Seller");
            LoginPage LoginObj = new LoginPage(driver);
            LoginObj.LogInPortal();
            
            var ProfilePageobj = new ProfilePage();
            ProfilePageobj.SearchSkillByTitle();
            var SearchSkillobj = new SearchSkillPage();
            SearchSkillobj.RefineSkillSearch();
            SearchSkillobj.ClickOnTargetSkill();
            //var TargetSkillobj = new TargetSkillPage();
            
            var NotificationObj = new Dashboard();
            NotificationObj.SendRequest();
            test.Log(Status.Info, "Notification Sent Successfully");
            
            NotificationObj.ValidateSendRequest();
            test.Log(Status.Pass, "Test Case: Notification Request to Seller, Passed");
        }

        //Validate if user's on Dashboard (Notification)
        [Test, Description("Test Case: SelectAll Notifications"), Order(2)]
        public void SelectAllNotifications()
        {
            test = extent.CreateTest("SelectAll Notifications").Info("Verifying, Select All Notifications");
            LoginPage LoginObj = new LoginPage(driver);
            LoginObj.LogInPortal();
                       
            Dashboard NotificationObj = new Dashboard();
            NotificationObj.SelectAllNotification();
            test.Log(Status.Info, "All Notifications Selected");

            NotificationObj.ValidateSelectNotification();
            test.Log(Status.Info, "Test Case:SelectAll Notifications, Passed");
        }

        [Test, Description("Test Case: UnSelectAll Notifications"), Order(3)]
        public void UnSelectAllNotifications()
        {
            test = extent.CreateTest("UnSelectAll Notifications").Info("Verifying, UnSelectAll Notifications");

            LoginPage LoginObj = new LoginPage(driver);
            LoginObj.LogInPortal();

            var NotificationObj = new Dashboard();
            NotificationObj.UnSelectAllNotifications();
            test.Log(Status.Info, "All Notifications Unselected");

            NotificationObj.ValidateUnSelectNotification();
            test.Log(Status.Info, "Validation for UnSelectAll done");
            test.Log(Status.Info, "Test Case: UnSelectAll Notifications, Passed");
        } 

        [Test, Description("Test Case: Mark All As Read"), Order(4)]
        public void MarkAllAsRead()
        {
            test = extent.CreateTest("Mark All As Read").Info("Verifying Mark All As Read");
            LoginPage LoginObj = new LoginPage(driver);
            LoginObj.LogInPortal();

            var NotificationObj = new Dashboard();
            NotificationObj.MarkALLasRead();
            test.Log(Status.Info, "Marked All Notifications as Read");

            NotificationObj.ValidateMessagesRead();
            test.Log(Status.Info, "Validation for Mark All Notifications as Read, done");
            test.Log(Status.Info, "Test Case: Mark All As Read, Passed");
        }

        [Test, Description("Test Case: Delete Selection"), Order(8)]
        public void DeleteSelection()
        {
            test = extent.CreateTest("Delete Selection").Info("Verifying Delete selected Notifications");
            LoginPage LoginObj = new LoginPage(driver);
            LoginObj.LogInPortal();
            
            var NotificationObj = new Dashboard();
            NotificationObj.DeleteNotificationMessage();
            test.Log(Status.Info, "Deleted selected Notifications");

            NotificationObj.ValidateDeleteMessage();
            test.Log(Status.Info, "Validation for Delete selected Notifications, done");
            test.Log(Status.Info, "Test Case:Deleted selected Notifications, Passed");
        }

        [Test, Description("Test Case: Load More"),Order(6)]
        public void LoadMore()
        {
            test = extent.CreateTest("Load More").Info("Verifying Load More Notifications");
            LoginPage LoginObj = new LoginPage(driver);
            LoginObj.LogInPortal();

            var NotificationObj = new Dashboard();
            NotificationObj.LoadMoreNotifications();
            test.Log(Status.Info, "Load More Notifications");
            test.Log(Status.Info, "Validation for Load More Notifications, done");
            test.Log(Status.Info, "Test Case:Load More Notifications, Passed");
        }

        [Test, Description("Test Case: Show Less"), Order(7)]
        public void ShowLess()
        {
            test = extent.CreateTest("Show Less").Info("Verifying Show Less Notifications");
            LoginPage LoginObj = new LoginPage(driver);
            LoginObj.LogInPortal();

            var NotificationObj = new Dashboard();
            NotificationObj.ShowLessNotifications();
            test.Log(Status.Info, "Show Less Notifications");
            test.Log(Status.Info, "Validation for Show Less Notifications, done");
            test.Log(Status.Info, "Test Case:Show Less Notifications, Passed");
        }

        [Test, Description("Test Case: Send Chat Message"), Order(9)]
        public void ChatMessageSend()
        {
            test = extent.CreateTest("Send Chat Message").Info("Verifying Send Chat Message");
            LoginPage LoginObj = new LoginPage(driver);
            LoginObj.LogInPortal();

            Message ChatObj = new Message(driver);
            ChatObj.SendChatMessage();
            test.Log(Status.Info, "Send Chat Message");

            ChatObj.ValidateChatMessageSent();
            test.Log(Status.Info, "Validation for Send Chat Message done");
            test.Log(Status.Info, "Test Case:Send Chat Message, Passed");
        }

        [TearDown]
        public void AfterClass()
        {
            //StackTrace details for failed Testcases
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                test.Log(Status.Fail, stackTrace + errorMessage);
                test.Log(Status.Fail, "ScreenShot of the Error: " + SaveScreenShotClass.SaveScreenshot(driver, $"{test} Fail"));
            }

            else if (status == TestStatus.Passed)
            {
                test.Log(Status.Pass, stackTrace + status);
                test.Log(Status.Pass, "ScreenShot: " + SaveScreenShotClass.SaveScreenshot(driver, $"{test} Pass"));
            }
            
            driver.Quit();
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            //End Report
            extent.Flush();
        }
    }
}
