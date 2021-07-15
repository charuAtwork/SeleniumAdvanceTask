using SkillSharePortal_ServiceList.Global;
using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;

namespace SkillSharePortal_ServiceList.Pages
{
    class NavigateToPage : GlobalDefinitions
    {
        IWebElement driver;
        public NavigateToPage(IWebDriver driver)    
        {
            PageFactory.InitElements(driver, this);
        }

        //Navigate to Manage Listing URL
        string ManageListingUrl = "http://localhost:5000/Home/ListingManagement";

        //Locate "Share Skill" button on Profile page
        [FindsBy(How = How.XPath, Using = "//*[@id='account-profile-section']/div/section[1]/div/div[2]/a")]
        private IWebElement ShareSkill { get; set; }

        //Locate "Manage Listing" tab on Profile Page
        [FindsBy(How = How.XPath, Using = "//*[@id='account-profile-section']/div/section[1]/div/a[3]")]
        private IWebElement ManageListings { get; set; }

        //Locate "Dashboard" on Profile Page
        [FindsBy(How = How.XPath, Using = "//*[@id='notification-section']/section[1]/div/a[1]")]
        private IWebElement DashboardNotify { get; set; }

        

        public void ClickShareSkill()
        {
            //Click on "ShareSkill"
            ShareSkill.Click();
        }

        public void ClickManageListing()
        {
            //Click on "ManageListing"
            ManageListings.Click();
        }

        public void Dashboard()
        {
           //Click on DashBoard
           DashboardNotify.Click();
        }
    }
}
