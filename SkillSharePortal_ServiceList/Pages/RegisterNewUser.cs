using System;
using System.Collections.Generic;
using System.Text;
using SkillSharePortal_ServiceList.Global;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using ExcelDataReader;

namespace SkillSharePortal_ServiceList.Pages
{
    class RegisterNewUser : GlobalDefinitions
    {
        IWebDriver driver;
        public RegisterNewUser(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
     
        }

        #region  Initialize Web Elements 
        //Locate "Join" button on Home Page
        [FindsBy(How = How.XPath, Using = "//*[@id='home']/div/div/div[1]/div/button")] 
        private IWebElement Join { get; set; }

         //Locate "FirstName" Textbox
         [FindsBy(How = How.Name, Using = "firstName")]
         private IWebElement FirstName { get; set; }

         //Locate "LastName" Textbox
         [FindsBy(How = How.Name, Using = "lastName")]
         private IWebElement LastName { get; set; }

          //Locate "Email" Textbox
          [FindsBy(How = How.Name, Using = "email")]
          private IWebElement Email { get; set; }

          //Locate "Password" Textbox
          [FindsBy(How = How.Name, Using = "password")]
          private IWebElement Password { get; set; }

          //Locate "Confirm Password" Textbox
          [FindsBy(How = How.Name, Using = "confirmPassword")]
          private IWebElement ConfirmPassword { get; set; }

          //Locate "Term and Conditions"
          [FindsBy(How = How.Name, Using = "terms")]
          private IWebElement Checkbox { get; set; }

          //Locate "Join" button
          [FindsBy(How = How.XPath, Using = "//*[@id='submit-btn']")]
          private IWebElement JoinBtn { get; set; }
            #endregion

          public void NewUser()
          {
                //Populate the excel data
                ExcelLib.PopulateInCollection(ExcelPath, "SignUp");

                driver.Navigate().GoToUrl(ExcelLib.ReadData(2, "Url"));

                //Click on Join button
                 Join.Click();

                //Enter FirstName
                FirstName.SendKeys(ExcelLib.ReadData(2, "FirstName"));

                //Enter LastName
                LastName.SendKeys(ExcelLib.ReadData(2, "LastName"));

                //Enter Email
                Email.SendKeys(ExcelLib.ReadData(2, "Email_ID"));

                //Enter Password
                Password.SendKeys(ExcelLib.ReadData(2, "Password"));

                //Enter Password again to confirm
                ConfirmPassword.SendKeys(ExcelLib.ReadData(2, "ConfirmPswd"));

                //Click on Checkbox
                Checkbox.Click();

                //Click on join button to Sign Up
                JoinBtn.Click();
          }
        /* public void ValidateNewUser()
         {
            //Login to loginPortal
            .LogInPortal();
         }*/

    }
}
