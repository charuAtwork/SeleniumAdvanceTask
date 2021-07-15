using System;
using System.Collections.Generic;
using System.Text;
using SkillSharePortal_ServiceList.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using System.Linq;
using System.Threading.Tasks;


namespace SkillSharePortal_ServiceList.Pages
{
    class ManageRequestPage
    {                
        public ManageRequestPage()
        {
                PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

            [FindsBy(How = How.XPath, Using = "//*[@id='service - search - section']/section[1]/div/div[1]")]
            private IWebElement ManageRequestHangover { get; set; }
            [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Received Requests')]")]
            private IWebElement ManageReceivedBtn { get; set; }
            [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sent Requests')]")]
            private IWebElement ManageSentBtn { get; set; }

            //find target table tr-received request
            [FindsBy(How = How.XPath, Using = "//*[@id='received-request-section']/div[2]/div[1]/table/tbody/tr")]
            private List<IWebElement> ReceivedTableTr { get; set; }
            //find target table tr - sent request
            [FindsBy(How = How.XPath, Using = "//*[@id='sent-request-section']/div[2]/div[1]/table/tbody/tr")]
            private List<IWebElement> SentTableTr { get; set; }

            //Actions
            [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sent Requests')]")]
            private IWebElement ActionAcceptBtn { get; set; }

            [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Decline')]")]
            private IWebElement ActionDeclineBtn { get; set; }

            [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Complete')]")]
            private IWebElement ActionCompleteBtn { get; set; }

            [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Review')]")]
            private IWebElement ActionReviewBtn { get; set; }
            [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Withdraw')]")]
            private IWebElement ActionWithdrawBtn { get; set; }

            internal void GotoReceivedOrSentRequest()
            {
                GlobalDefinitions.ExcelLib.PopulateInCollection(GlobalDefinitions.ExcelPath, "SearchSkill");
                GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath("//*[@id='service - search - section']/section[1]/div/div[1]"), 10);
                //hangover
                Actions action = new Actions(GlobalDefinitions.driver);

                if (GlobalDefinitions.ExcelLib.ReadData(2, "SkillType") == "Received Request")
                {
                    action.MoveToElement(ManageReceivedBtn).Click();
                }
                if (GlobalDefinitions.ExcelLib.ReadData(2, "SkillType") == "Sent Request")
                {
                    action.MoveToElement(ManageSentBtn).Click();
                }
            }

            internal void ActionForReceivedRequest()
            {
                //when I am on received request page
                GlobalDefinitions.ExcelLib.PopulateInCollection(GlobalDefinitions.ExcelPath, "ReceivedRequest");

                String StatusReceived = GlobalDefinitions.ExcelLib.ReadData(2, "Status");
                String ActionWhenPending = GlobalDefinitions.ExcelLib.ReadData(2, "Actions");

                if (StatusReceived == "Pending")
                {
                    if (ActionWhenPending == "Accept")
                    {
                        ActionAcceptBtn.Click();
                    }

                    if (ActionWhenPending == "Decline")
                    {
                        ActionDeclineBtn.Click();
                    }
                }
                if (StatusReceived == "Accepted")
                {
                    ActionCompleteBtn.Click();
                }
            }

            internal void ActionForSentRequest()
            {
                //when I am on received request page
                GlobalDefinitions.ExcelLib.PopulateInCollection(GlobalDefinitions.ExcelPath, "SentRequest");

                String StatusReceived = GlobalDefinitions.ExcelLib.ReadData(2, "Status");
                String ActionWhenPending = GlobalDefinitions.ExcelLib.ReadData(2, "Actions");

                if (StatusReceived == "Completed")
                {
                    ActionReviewBtn.Click();
                }
                if (StatusReceived == "Pending")
                {
                    ActionWithdrawBtn.Click();
                }
            }






        }
    }


