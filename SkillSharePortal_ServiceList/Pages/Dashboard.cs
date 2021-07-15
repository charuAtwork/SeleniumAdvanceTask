using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SkillSharePortal_ServiceList.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SkillSharePortal_ServiceList.Pages
{
    public class Dashboard : GlobalDefinitions
    {
        //IWebDriver driver;
        int rownumber;
        int countMsg, countMsgUnRead, CountNotify;
        public Dashboard()
        {

            //Pass driver as an argument to InitElement() method as the driver from Utilities will be used to instantiate the Web Elements
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);

        }

        //Request message to the Provider - Request Skill Trade
        [FindsBy(How = How.XPath, Using = "//*[@id='service-detail-section']/div[2]/div/div[2]/div[2]/div[2]/div/div[2]/div/div[1]/textarea")]
        private IWebElement RequestToSeller { get; set; }

        // Request Button
        [FindsBy(How = How.XPath, Using = "//*[@id='service-detail-section']/div[2]/div/div[2]/div[2]/div[2]/div/div[2]/div/div[3]")]
        private IWebElement RequestButton { get; set; }

        //Confirmation Input box
        [FindsBy(How = How.XPath, Using = "/html/body/div[4]/div/div[3]/button[1]")]
        private IWebElement YesButton { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[4]/div/div[3]/button[2]")]
        private IWebElement NoButton { get; set; }

        //Dashboard
        [FindsBy(How = How.XPath, Using = "//*[@id='service-detail-section']/section[1]/div/a[1]")]
        private IWebElement DashboardPage { get; set; }

        //MarkAllAsRead Option on Notification dropdown
        [FindsBy(How = How.XPath, Using = "//*[@id='notification-section']/div[1]/div[2]/div/div/div/span/div/div[1]/div[1]/center/a")]
        private IWebElement MarkAllAsReadDropdown { get; set; }

        //ShowLess Option on Notification frame on Dashboard
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'...Show Less')]")]
        private IWebElement ShowLess { get; set; }

        //LoadMore Option
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Load More...')]")]
        private IWebElement LoadMore { get; set; }

        //SelectAllIcon Option 
        [FindsBy(How = How.XPath, Using = ".//div[@class ='ui icon basic button']")]
        private IWebElement SelectAllIcon1 { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[@class='mouse pointer icon']")]
        private IWebElement SelectAllIcon { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#notification-section>div.ui.fluid.container>div>div>div.seven.wide.column>div.ui.menu>div:nth-child(1)")]
        private IWebElement SelectAllIcon2 { get; set; }
        
        //UnSelectAll Icon Option
        [FindsBy(How = How.XPath, Using = "//*[@id='notification-section']/div[2]/div/div/div[3]/div[1]/div[2]/i")]
        private IWebElement UnSelectAllIcon { get; set; }

        //DeleteSelectionIcon Option
        [FindsBy(How = How.XPath, Using = "//*[@id='notification-section']/div[2]/div/div/div[3]/div[1]/div[3]")]
        private IWebElement DeleteSelectionIcon { get; set; }

        //MarkSelectionAsReadIcon Option
        [FindsBy(How = How.XPath, Using = "//*[@id='notification-section']/div[2]/div/div/div[3]/div[1]/div[4]")]
        private IWebElement MarkSelectionAsReadIcon { get; set; }

        //Notification Message
        [FindsBy(How = How.XPath, Using = "//div[@class='ns-box-inner']")]
        private IWebElement NotifyMessage { get; set; }

        //Notification Record
        //[FindsBy(How = How.XPath, Using = ".//*[@class='item link']")]
        //private IList<IWebElement> NotificationRecord { get; set; }

        //CheckBox Notification
        [FindsBy(How = How.XPath, Using = "//input[@type='checkbox']")]
        private IList<IWebElement> CheckBoxNotify { get; set; }

        //Notification dropdown
        //[FindsBy(How = How.XPath, Using = "//div[@class='ui top left pointing dropdown item']/i[2]")]

        [FindsBy(How = How.XPath, Using = "//*[@id='account-profile-section']/div/div[1]/div[2]/div/div")]
        private IWebElement NotificationDropdown { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='ns-box-inner']")]
        private IWebElement ConfirmDeleteMessage { get; set; }

        //SeeAll Option on Notification dropdown
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'See All...')]")]
        private IWebElement SeeAll { get; set; }

        //Locate Notification Menu on Dashboard
        [FindsBy(How = How.XPath, Using = "//*[@id='notification-section]/div[2]/div/div/div[3]/div[1]")]
        private IWebElement NotificationMenu { get; set; }

        //[FindsBy(How=How.XPath, Using = "//*[@id='notification-section']/div[2]/div/div/div[3]/div[2]")]
        //public static IWebElement NotificationGrid { get; set; }

        public IList<IWebElement> NotificationRecord = driver.FindElements(By.XPath(".//div[@class='item link']"));

        //Send Service Request to Seller
        public void SendRequest()
        {
            //Populate the data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPath, "Notification");
            RequestToSeller.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "NotificationMsg"));
            RequestButton.Click();
            GlobalDefinitions.ElementIsVisible(GlobalDefinitions.driver, "XPath", "/html/body/div[4]/div");
            YesButton.Click();
        }

        //SelectAll Notification Messages
        public void SelectAllNotification()
        {
            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//*[@id='account-profile-section']/div/div[1]/div[2]/div/div");
            NotificationDropdown.Click();
            ImplicitWait(5);
            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//a[contains(text(),'See All...')]");
            SeeAll.Click();
            Thread.Sleep(200);
            //ImplicitWait(5);
            countMsg = CheckBoxNotify.Count();
           // countMsg = NotificationRecord.Count();
            if (countMsg < 5)
            {
                Thread.Sleep(100);
                SelectAllIcon.Click();
                Thread.Sleep(100);
                Assert.Pass("TestCase to Select All Notifications has Passed");
            }
            else
            {
                try
                {
                    do
                    {
                        Thread.Sleep(300);
                        LoadMore.Click();
                        Thread.Sleep(900);
                        SelectAllIcon.Click();
                    } while (LoadMore.Displayed);

                }

                catch (NoSuchElementException e)
                {
                    TestContext.WriteLine("All the notifications have been loaded,No more Notification Messages to be loaded");
                }

            }
        }

        //UnselectAll Notification Messages
        public void UnSelectAllNotifications()
        {
            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//*[@id='account-profile-section']/div/div[1]/div[2]/div/div");
            NotificationDropdown.Click();

            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//a[contains(text(),'See All...')]");
            SeeAll.Click();

            Thread.Sleep(900);
            try
            {
                countMsg = CheckBoxNotify.Count();

               // if ((countMsg <= 5) && (!LoadMore.Displayed))
                if(countMsg<=5)
                {
                    Thread.Sleep(600);
                    SelectAllIcon.Click();
                    
                }
                else
                {
                    do
                    {
                        LoadMore.Click();
                        Thread.Sleep(900);
                        SelectAllIcon.Click();
                    } while (LoadMore.Displayed);

                }
            }
            catch (NoSuchElementException e)
            {
                TestContext.WriteLine("All the notification messages have been loaded,No more Notification Messages to be loaded");
            }

            UnSelectAllIcon.Click();

        }

        //Delete selected Notification Message ONLY(not All)
        public void DeleteNotificationMessage()
        {
            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//*[@id='account-profile-section']/div/div[1]/div[2]/div/div");
            NotificationDropdown.Click();

            ImplicitWait(9);

            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//a[contains(text(),'See All...')]");
            SeeAll.Click();

            try
            {
                countMsg = CheckBoxNotify.Count();

                if ((countMsg <= 5) && (!LoadMore.Displayed))
                    Delete();
                else
                {
                    do
                    {
                        LoadMore.Click();
                        Thread.Sleep(900);

                    } while (LoadMore.Displayed);
                }
            }

            catch (NoSuchElementException e)
            {
                TestContext.WriteLine("All the notification messages have been loaded,No more Notification Messages to be loaded");
            }

            finally
            {
                TestContext.WriteLine("Call to Delete Method");
                Delete();
            }

        }

        public void Delete()
        {
            countMsg = CheckBoxNotify.Count();

            //Select rows for Deletion
            for (int i = 1; i <= countMsg; ++i)
            {

                string SearchMsg = driver.FindElement(By.XPath("//*[@id='notification-section']/div[2]/div/div/div[3]/div[2]/span/span/div/div[" + i + "]/div/div/div[2]/div[1]/a/div[1]")).Text;

                ImplicitWait(2);

                GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPath, "Notification");
                string SearchforString = GlobalDefinitions.ExcelLib.ReadData(4, "DeletionCriteria");
                bool SearchResult = SearchMsg.Contains(SearchforString);

                if (SearchResult)
                {
                    CheckBoxNotify[i - 1].Click();
                }
            }
            Thread.Sleep(200);
            DeleteSelectionIcon.Click();
        }

        internal void MarkAsRead()
        {
            // get the count of notification messages
            countMsg = CheckBoxNotify.Count();

            //Mark All Notification Messages listed as Read
            
                for (int i = 0; i < countMsg; i++)
                {
                    string MessageAsRead = CheckBoxNotify[i].GetCssValue("font-weight");
                    Thread.Sleep(200);

                    if (MessageAsRead == "700" || MessageAsRead == "400")
                    {
                        CheckBoxNotify[i].Click();
                        //++countMsgUnRead;
                    }
                }
            MarkSelectionAsReadIcon.Click();
        }

        //MarkSelectionAsRead Notification Messages - MARK ALL THE MESSAGES AS READ
        public void MarkALLasRead()
        {
            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//*[@id='account-profile-section']/div/div[1]/div[2]/div/div");
            NotificationDropdown.Click();

            ImplicitWait(9);

            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//a[contains(text(),'See All...')]");
            //SeeAll Notification Messages
            SeeAll.Click();
            int countMsg;

            //LoadMore
            try
            {
                countMsg = CheckBoxNotify.Count();
                if ((countMsg <= 5) && (!LoadMore.Displayed))
                    MarkAsRead();
                else
                {
                    do
                    {
                        LoadMore.Click();
                        Thread.Sleep(500);
                    } while (LoadMore.Displayed);
                }
            }

            catch (NoSuchElementException e)
            {
                TestContext.WriteLine("All the notification messages have been loaded,No more Notification Messages to be loaded");
            }

            finally
            {
                MarkAsRead();
            }    

        }
    
        public void LoadMoreNotifications()
        {
            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//*[@id='account-profile-section']/div/div[1]/div[2]/div/div");
            NotificationDropdown.Click();
            ImplicitWait(5);

            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//a[contains(text(),'See All...')]");
            SeeAll.Click();
            ImplicitWait(5);

            try
            {
                countMsg = NotificationRecord.Count();
                //TestContext.WriteLine("countMsg = ", countMsg);

                if ((countMsg <= 5) && (!LoadMore.Displayed))
                    TestContext.WriteLine("No notifications to load");
                else
                {
                    do
                    {
                        LoadMore.Click();
                        Thread.Sleep(700);
                        //ImplicitWait(9);
                    } while (LoadMore.Displayed);
                }
            }
            catch (NoSuchElementException e)
            {
                Assert.Pass("Test Case to LoadMore Notification has Passed");
                TestContext.WriteLine("All the notifications have been loaded,No more Notification Messages to be loaded");
            }
        }
        public void ShowLessNotifications()
        {
            bool IsShowLess = false;
            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//*[@id='account-profile-section']/div/div[1]/div[2]/div/div");
            NotificationDropdown.Click();
            ImplicitWait(2);

            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//a[contains(text(),'See All...')]");
            SeeAll.Click();
            ImplicitWait(2);

            //Click on Load More, Load all the notifications
            int countMsg = NotificationRecord.Count; ;
            try
            {
                if ((countMsg <= 5) && (!LoadMore.Displayed))
                
                    TestContext.WriteLine("No Notifications to Load");
                
                else
                {
                    do
                    {
                        LoadMore.Click();
                        Thread.Sleep(200);
                        //ImplicitWait(7);
                    } while (LoadMore.Displayed);
                }
            }

            catch (NoSuchElementException e)
            {
                TestContext.WriteLine("No More Notification Messages to Load");
            }

            finally
            {
                ImplicitWait(2);
                try
                {
                    //Click on Show Less, click until it stops to display
                    do
                    {
                        ShowLess.Click();
                        Thread.Sleep(200);
                        //ImplicitWait(2);

                    } while (ShowLess.Displayed);
                    IsShowLess = false;
                }

                catch (NoSuchElementException)
                {
                    TestContext.WriteLine("Showing Lastest 5 Notification Messages");
                    if (IsShowLess == false)
                        Assert.Pass("Test to verify Show Less - Passed");
                    else
                        Assert.Fail("Test to verify Show Less - Failed");
                }

                /*catch (NoSuchElementException e)
                {
                    TestContext.WriteLine("Showing Lastest 5 Notification Messages");
                } */

            }
        }

        //VALIDATE UnSELECTALL NOTIFICATION
        public void ValidateUnSelectNotification()
        {
            //VALIDATE
            //int CountNotify = 0;
            CountNotify = CheckBoxNotify.Count;

            for (int i = 1; i <= CountNotify; i++)
            {

                if (!CheckBoxNotify[i].Selected)
                {

                    Assert.Pass("Test to verify UnSelectAll was Successful");
                }
                else
                {
                    Assert.Fail("Test to verify UnSelectAll was UnSuccessful");
                    TestContext.WriteLine("TestCase Passed");
                }
            }
        }

        //VALIDATE - SEND REQUEST
        public void ValidateSendRequest()
        {
            //Validate Notification sent
            GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath("//div[@class='ns-box-inner']"), 10);
            string SuccessNotifyMsgActual = NotifyMessage.Text;
            string SuccessNotifyMsgExpected = "Request sent";

            Assert.That(SuccessNotifyMsgActual, Is.EqualTo(SuccessNotifyMsgExpected));
        }

        //VALIDATE SELECT NOTIFICATION
        public void ValidateSelectNotification()
        {
            //VALIDATE
            int CountNotify = CheckBoxNotify.Count;

            TestContext.WriteLine("Count is ", CountNotify);

            for (int i = 1; i <= CountNotify; i++)
            {
                if (CheckBoxNotify[i].Selected)
                {
                    Assert.Pass("Test to verify SelectAll was Successful");
                    TestContext.WriteLine("TestCase Passed");
                }
                else
                {
                    Assert.Fail("Test to verify SelectAll was UnSuccessful");
                }
            }
        }

        //VALIDATE - Messages Read
        public void ValidateMessagesRead()
        {
            //Validate by verifing that all the messages have font weight 400
            /* countMsg = CheckBoxNotify.Count();
             for (int i = 0; i < countMsg; i++)
             {
                 string MsgAsRead = CheckBoxNotify[i].GetCssValue("font-weight");
                 if (MsgAsRead == "400")
                     Assert.Pass("Messages Marked as Read");

             } */
            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//div[@class='ns-box-inner']");
            string ActualMessage = ConfirmDeleteMessage.Text;
            string ExpectedMessage = "Notification updated";
            Assert.That(ActualMessage, Is.EqualTo(ExpectedMessage));

            TestContext.WriteLine("Notification Messages Marked as Read successfully");
        } //end of Validate

        //VALIDATE - Delete Message
        public void ValidateDeleteMessage()
        {
            GlobalDefinitions.ElementIsVisible(driver, "XPath", "//div[@class='ns-box-inner']");
            string ActualMessage = ConfirmDeleteMessage.Text;
            string ExpectedMessage = "Notification updated";
            Assert.That(ActualMessage, Is.EqualTo(ExpectedMessage));

            TestContext.WriteLine("Notification Message deleted successfully");


        }

    }  
}


