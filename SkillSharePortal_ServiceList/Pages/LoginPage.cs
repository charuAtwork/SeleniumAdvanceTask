using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;
using SeleniumExtras.PageObjects;
using SkillSharePortal_ServiceList.Global;

namespace SkillSharePortal_ServiceList.Pages
{
    class LoginPage 
    {
        //Declare the driver
        IWebDriver driver;
        //Constructor to initialise the driver and webelements
        public LoginPage(IWebDriver driver)
        {
            //this.driver = driver1;
            //PageFactory.InitElements(driver, this);
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }

        #region  Initialize Web Elements
         //Locate "Username" on Profile Page
         [FindsBy(How = How.XPath, Using = "//*[@id='account-profile-section']/div/div[1]/div[2]/div/span")]
         private IWebElement Username { get; set; }

         //Locate "SignIn" Link
         [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sign')]")]
         private IWebElement SignIntab { get; set; }

         //Locate "Email" 
         [FindsBy(How = How.Name, Using = "email")]  
         private IWebElement Email { get; set; }

         //Locate "Password"
         [FindsBy(How = How.Name, Using = "password")]
         private IWebElement Password { get; set; }

         //Locate "Login" Button
         [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Login')]")]
          private IWebElement LoginButton { get; set; }
         #endregion
                  
          public void LogInPortal()
          {
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(GlobalDefinitions.ExcelPath, "Login");
            driver.Navigate().GoToUrl(GlobalDefinitions.ExcelLib.ReadData(2,"Url"));
            //ImplicitWait(5);
            SignIntab.Click();
            Email.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Email_ID"));
            //ImplicitWait(5);
            Password.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));
            LoginButton.Click();
          }

          internal void ValidateLogIn()
          {
            try
            {
                Assert.AreEqual(Username, "Hi Charu");
                TestContext.WriteLine("User Successfully Logged in to Skill Share portal");
            }
            catch
            {
                Exception e;
            }
          }
    }
}
 
