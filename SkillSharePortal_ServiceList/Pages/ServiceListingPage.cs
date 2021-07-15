using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SkillSharePortal_ServiceList.Global;
//using RelevantCodes.ExtentReports;

namespace SkillSharePortal_ServiceList.Pages
{
    class ServiceListingPage
    {
        //rownumber for reading data from excel
        int rownumber = 0;
        IWebDriver driver;
        public ServiceListingPage(IWebDriver driver, int rownumber)
        {
            this.driver = driver;
            //Pass driver as an argument to InitElement() method as the driver from Utilities will be used to instantiate the Web Elements
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
            this.rownumber = rownumber;
        }

        /*************************************************Locate the WebElements on ShareSkill*********************************************/

        //Locate ShareSkill Button
        [FindsBy(How = How.LinkText, Using = "Share Skill")]
        private IWebElement ShareSkillButton { get; set; }

        //Locate Title
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement Title { get; set; }

        //Locate the Description
        [FindsBy(How = How.Name, Using = "description")]
        private IWebElement Description { get; set; }

        //Locate Category Dropdown
        [FindsBy(How = How.Name, Using = "categoryId")]
        private IWebElement CategoryDropDown { get; set; }

        //Locate SubCategory Dropdown
        [FindsBy(How = How.Name, Using = "subcategoryId")]
        private IWebElement SubCategoryDropDown { get; set; }

        //Locate Tag names in textbox
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[4]/div[2]/div/div/div/div/input")]
        private IWebElement Tags { get; set; }

        //Locate the Service types
        [FindsBy(How = How.XPath, Using = "//input[@name='serviceType']")]
        private IList<IWebElement> AllServiceTypeOptions { get; set; }

        //Locate the Location Type
        [FindsBy(How = How.Name, Using = "locationType")]
        private IList<IWebElement> AllLocationTypeOptions { get; set; }

        //Locate Start Date dropdown
        [FindsBy(How = How.Name, Using = "startDate")]
        private IWebElement StartDateDropDown { get; set; }

        //Locate on End Date dropdown
        [FindsBy(How = How.Name, Using = "endDate")]
        private IWebElement EndDateDropDown { get; set; }

        //Storing the table of available days
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[@class='ui container']/div[@class='listing']/form[@class='ui form']/div[7]/div[2]/div[1]")]
        private static IWebElement CalendarTable { get; set; }

        //Locate the Days CheckBoxes
        //[FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[7]/div[2]/div/div[2]/div[1]/div/input")]
        [FindsBy(How = How.Name, Using = "Available")]
        private IList<IWebElement> ListOfDaysAvailable { get; set; }

        //Locate StartTime dropdown
        [FindsBy(How = How.Name, Using = "StartTime")]
        private IList<IWebElement> StartTimeDropDown { get; set; }

        //Locate EndTime dropdown
        //[FindsBy(How = How.XPath, Using = "//div[3]/div[3]/input[1]")]
        [FindsBy(How = How.Name, Using = "EndTime")]
        private IList<IWebElement> EndTimeDropDown { get; set; }

        //Locate Skill Trade option
        [FindsBy(How = How.Name, Using = "skillTrades")]
        private IList<IWebElement> AllSkillTradeOption { get; set; }

        //Locate Skill Exchange
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[4]/div/div/div/div/div/input")]

        //[FindsBy(How = How.CssSelector, Using = "#service-listing-section>div.ui.container>div>form>div:nth-child(8)>div:nth-child(4)>div>div>div>div>div>input")]
        private IWebElement SkillExchange { get; set; }

        //Locate Credit
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Amount']")]
        private IWebElement CreditAmount { get; set; }

        //Locate Work Samples
        //*[@id="service-listing-section"]/div[2]/div/form/div[9]/div/div[2]/section/div/label/div/span/i
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[9]/div/div[2]/section/div/label/div/span/i")]
        private IWebElement WorkSample { get; set; }

        //Locate Active/Hidden option
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[10]/div[2]/div/div[1]/div/input")]
        private IWebElement ActiveOption { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[10]/div[2]/div/div[2]/div/input")]
        private IWebElement HiddenOption { get; set; }

        //Locate Save button
        [FindsBy(How = How.XPath, Using = "//input[@value='Save']")]
        private IWebElement Save { get; set; }
        //public object Driver { get; }

        /******************************************Adding ServiceDetails Methods Definitions*****************************************************/
        internal void CategoryDrpDown(int rownumber)
        {
            SelectElement select = new SelectElement(CategoryDropDown);
            string Category = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Category");
            select.SelectByText(Category);
        }
        internal void SubCategoryDrpDown(int rownumber)
        {
            SelectElement select = new SelectElement(SubCategoryDropDown);
            string SubCategory = GlobalDefinitions.ExcelLib.ReadData(rownumber, "SubCategory");
            select.SelectByText(SubCategory);
        }
        internal void ServiceTypeOptions(int rownumber)
        {
            String UserInputServiceType = GlobalDefinitions.ExcelLib.ReadData(rownumber, "ServiceType");

           // String ValueServiceType = AllServiceTypeOptions[0].Text;
            if (UserInputServiceType == "Hourly")
                AllServiceTypeOptions[0].Click();
            else
                AllServiceTypeOptions[1].Click();
        }
        internal void LocationTypeOptions(int rownumber)
        {
            String UserInputLocationType = GlobalDefinitions.ExcelLib.ReadData(rownumber, "LocationType");

            //String ValueLocationType = AllLocationTypeOptions[0].Text;
            if (UserInputLocationType == "On-site")
                AllLocationTypeOptions[0].Click();
            else
                AllLocationTypeOptions[1].Click();
        }
        internal void SkillTradeOptions(int rownumber)
        {
            String UserInputSkillTrade = GlobalDefinitions.ExcelLib.ReadData(rownumber, "SkillTrade");

            if (UserInputSkillTrade == "Skill-exchange")
            {
                AllSkillTradeOption[0].Click();
            }
            else
            {
                AllSkillTradeOption[1].Click();
            }
        }

        internal void SkillTags(int rownumber)
        {
            String UserInputSkillTrade = GlobalDefinitions.ExcelLib.ReadData(rownumber, "SkillTrade");
            string TagsCount = GlobalDefinitions.ExcelLib.ReadData(rownumber, "SkillExchangeTags");

             //get the Number of Tags for Skill Exchange
            //int NoOfTags = Int32.Parse(TagsCount);

            if (UserInputSkillTrade == "Skill-exchange")
            {
                for (int i = 0; i < 3; i++)
                {
                    SkillExchange.SendKeys(GlobalDefinitions.ExcelLib.ReadData(rownumber+i, "Skill-exchange"));
                    SkillExchange.SendKeys(Keys.Return);
                }
            }
            else if (UserInputSkillTrade == "Credit")
            {
                CreditAmount.SendKeys(GlobalDefinitions.ExcelLib.ReadData(rownumber, "Credit"));
            }
        }
    

         string SelectDays(int rownumber)
         {
            GlobalDefinitions.ExcelLib.PopulateInCollection(GlobalDefinitions.ExcelPath, "ShareSkill");
            return GlobalDefinitions.ExcelLib.ReadData(rownumber, "Selectday");
         }
        
        internal void SelectDateValue(int rownumber)
        {
            var StartDate = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Startdate");
            StartDateDropDown.SendKeys(StartDate);

            var EndDate = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Enddate");
            EndDateDropDown.SendKeys(EndDate);
        }

        internal void DaysServiceAvailable(int rownumber)
        {
              //Read Days' data 
              var ServiceOfferedCount = GlobalDefinitions.ExcelLib.ReadData(rownumber, "DaysServiceOffered");
             //Console.WriteLine("ServiceOfferedCount = ", ServiceOfferedCount);
            // int Count = Int32.Parse(ServiceOfferedCount);
             //Console.WriteLine("Count = ", Count);
             var StartTime = "000000";
             var EndTime = "000000";

            for (int i = 1; i <= 7; i++)
            {
                rownumber = rownumber + 1;
                TestContext.WriteLine("RowNumber =  ",rownumber);

                if (SelectDays(rownumber) == "Sun")
                {
                    StartTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Starttime");
                    EndTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Endtime");
                    ListOfDaysAvailable[0].Click();
                    StartTimeDropDown[0].Click();
                    StartTimeDropDown[0].SendKeys(StartTime);
                    EndTimeDropDown[0].Click();
                    EndTimeDropDown[0].SendKeys(EndTime);
                    
                }
                else if (SelectDays(rownumber) == "Mon")
                {
                    StartTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Starttime");
                    EndTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Endtime");
                    ListOfDaysAvailable[1].Click();
                    StartTimeDropDown[1].Click();
                    StartTimeDropDown[1].SendKeys(StartTime);
                    EndTimeDropDown[1].Click();
                    EndTimeDropDown[1].SendKeys(EndTime);
                  
                }
                else if (SelectDays(rownumber) == "Tue")
                {
                    StartTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Starttime");
                    EndTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Endtime");
                    ListOfDaysAvailable[2].Click();
                    StartTimeDropDown[2].Click();
                    StartTimeDropDown[2].SendKeys(StartTime);
                    EndTimeDropDown[2].Click();
                    EndTimeDropDown[2].SendKeys(EndTime);
                   
                }
                else if (SelectDays(rownumber) == "Wed")
                {
                    StartTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Starttime");
                    EndTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Endtime");
                    ListOfDaysAvailable[3].Click();
                    StartTimeDropDown[3].Click();
                    StartTimeDropDown[3].SendKeys(StartTime);
                    EndTimeDropDown[3].Click();
                    EndTimeDropDown[3].SendKeys(EndTime);
                    
                }
                else if (SelectDays(rownumber) == "Thu")
                {
                    StartTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Starttime");
                    EndTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Endtime");
                    ListOfDaysAvailable[4].Click();
                    StartTimeDropDown[4].Click();
                    StartTimeDropDown[4].SendKeys(StartTime);
                    EndTimeDropDown[4].Click();
                    EndTimeDropDown[4].SendKeys(EndTime);
                    
                }
                else if (SelectDays(rownumber) == "Fri")
                {
                    StartTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Starttime");
                    EndTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Endtime");
                    ListOfDaysAvailable[5].Click();
                    StartTimeDropDown[5].Click();
                    StartTimeDropDown[5].SendKeys(StartTime);
                    EndTimeDropDown[5].Click();
                    EndTimeDropDown[5].SendKeys(EndTime);
                    
                }
                else if (SelectDays(rownumber) == "Sat")
                {
                    StartTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Starttime");
                    EndTime = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Endtime");
                    ListOfDaysAvailable[6].Click();
                    StartTimeDropDown[6].Click();
                    StartTimeDropDown[6].SendKeys(StartTime);
                    EndTimeDropDown[6].Click();
                    EndTimeDropDown[6].SendKeys(EndTime);
                    
                }
            }
        }

        internal void ServiceStatus (int rownumber)
        {
            string activeOp = GlobalDefinitions.ExcelLib.ReadData(rownumber, "Active");
            if (activeOp == "Active")
                ActiveOption.Click();
            else
                HiddenOption.Click();
        }

        /***************************************************ADD SERVICE DETAILS******************************************************/

        public void AddServiceData()
        {            
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(GlobalDefinitions.ExcelPath, "ShareSkill");

            //Read Test Data from the Excel sheet
            Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(rownumber, "Title"));
            Description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(rownumber, "Description"));

            CategoryDrpDown(rownumber);
            GlobalDefinitions.ImplicitWait(10);

            SubCategoryDrpDown(rownumber);
            GlobalDefinitions.ImplicitWait(5);

            Tags.SendKeys(GlobalDefinitions.ExcelLib.ReadData(rownumber, "Tags"));
            Tags.SendKeys(Keys.Return);

            //Input data for Service Type
            ServiceTypeOptions(rownumber);

            //Input data for Location Type
            LocationTypeOptions(rownumber);

            SelectDateValue(rownumber);

            DaysServiceAvailable(rownumber);

            SkillTradeOptions(rownumber);

            SkillTags(rownumber);

            ServiceStatus(rownumber);

            Save.Click();

            //navigate to next row
            rownumber++;
        }


        
    }
}



