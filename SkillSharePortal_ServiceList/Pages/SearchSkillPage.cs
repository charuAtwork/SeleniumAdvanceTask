using System;
using System.Collections.Generic;
using System.Text;
using SkillSharePortal_ServiceList.Global;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSharePortal_ServiceList.Pages
{
    class SearchSkillPage
    {
       public SearchSkillPage() => PageFactory.InitElements(Global.GlobalDefinitions.driver, this);

        [FindsBy(How = How.XPath, Using = ".//*[@placeholder='Search user']")]
        private IWebElement SearchByuserInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@score='1']")]
        private IWebElement SearchByuserResult { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@class='image']")]
        private IWebElement TargetResult { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@placeholder='Search skills']")]
        private IWebElement SearchSkillInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@class='search link icon']")]
        private IWebElement SearchSkillIcon { get; set; }

        internal void SearchSkillByUser()
        {

            GlobalDefinitions.ExcelLib.PopulateInCollection(GlobalDefinitions.ExcelPath, "SearchSkill");
            GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath(".//*[@placeholder='Search user']"), 10);
            SearchByuserInput.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "SkillUser"));
            GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath(".//*[@score='1']"), 10);
            SearchByuserInput.Click();

        }
        //Refine Search
        internal void RefineSkillSearch()
        {
            GlobalDefinitions.ExcelLib.PopulateInCollection(GlobalDefinitions.ExcelPath, "SearchSkill");
            //GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath(".//*[@placeholder='Search skills']"), 10);
            //GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath(".//*[@placeholder='Search user']"), 10);
            SearchByuserInput.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "SkillUser"));
            SearchSkillInput.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "SkillTitle"));
            SearchSkillInput.SendKeys(Keys.Enter);
        }

        internal void ClickOnTargetSkill()
        {
            GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath(".//*[@class='image']"), 10);
            TargetResult.Click();
        }



    }
}


