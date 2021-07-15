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
    public class Message : GlobalDefinitions
    {
        IWebDriver driver;

        public Message(IWebDriver driver)
        {
            this.driver = driver;
            //Pass driver as an argument to InitElement() method as the driver from Utilities will be used to instantiate the Web Elements
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }
                
        #region  Initialize Web Elements
        //Chat Link
        [FindsBy(How = How.XPath, Using = "//*[@id='account-profile-section']/div/div[1]/div[2]/div/a[1]")]
        private IWebElement Chat { get; set; }

        //Chat Message
        [FindsBy(How = How.XPath, Using = "//input[@id='chatTextBox']")]
        private IWebElement InputChatMessage { get; set; }

        //Send Button
        [FindsBy(How = How.XPath, Using = "//button[@id='btnSend']")]
        private IWebElement SendMessage { get; set; }

        //Chat Messages Displaybox
        [FindsBy(How = How.XPath, Using = "//div[@class='chatBoxWrapper']")]
        private IWebElement DisplayMessageBox { get; set; }

        //ChatRoom Search TextBox
        [FindsBy(How=How.XPath, Using = "//*[@id='chatRoomContainer']/div/div[1]/div/div[1]/input")]
        private IWebElement ChatRoomSearch { get; set; }

        //ChatRoom Search Icon
        [FindsBy(How = How.XPath, Using = "//*[@id='chatRoomContainer']/div/div[1]/div/div[1]/i")]
        private IWebElement ChatRoomSearchIcon { get; set; }

        
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
       
        private static IList<IWebElement> ChatTable = GlobalDefinitions.driver.FindElements(By.XPath("//div[@id='chatList']"));
        #endregion

        //When User exists in the Chatroom
        public void SearchChatRoom()
        {
            //Input UserName in Chat Room Search
            GlobalDefinitions.ExcelLib.PopulateInCollection(GlobalDefinitions.ExcelPath, "Notification");

            string ChatMsgRecvrUserName = GlobalDefinitions.ExcelLib.ReadData(2, "ChatMsgRecvrUsername");

            string ChatMsgSenderUserName = GlobalDefinitions.ExcelLib.ReadData(2, "ChatMsgSenderUsername");

            ChatRoomSearch.Clear();

            ChatRoomSearch.SendKeys(ChatMsgRecvrUserName);

            //Click on Search Result

            ChatRoomSearchIcon.Click();

            int countChatMsg = ChatTable.Count();

            for (int i=1;i<=countChatMsg;i++)
            {
               
                IWebElement ChatRecords = GlobalDefinitions.driver.FindElement(By.XPath("//div[@id='chatList']//div[@class='content']"));

                string ChatRecordUserName = ChatRecords.Text;
                ChatRecords.Click();
            }
        }

        public void SendChatMessage()
        {
            ImplicitWait(2);
            //Click on Chat
            Chat.Click();

            ImplicitWait(2);
            SearchChatRoom();

            //Get Chat Message
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPath, "Notification");
            ImplicitWait(2);
            
            string chat = GlobalDefinitions.ExcelLib.ReadData(2, "ChatMessage");
            
            ImplicitWait(2);
            InputChatMessage.Click();
            InputChatMessage.SendKeys(chat);

            //Click Send
            SendMessage.Click();

        }
        public void ValidateChatMessageSent()
        {
            driver.Navigate().Refresh();
            //In the Chat search, search for the Chat Message Sender

            ImplicitWait(2);
            SearchChatRoom();
            
            string ExpectedMsg = GlobalDefinitions.ExcelLib.ReadData(2, "ChatMessage");

            //In the ChatRoom search for the Message String

            // IWebElement Message = driver.FindElement(By.XPath("//div[@id='chatBox']//div/descendants::/div[contains(text(),'Thanks')]"));
            //IWebElement Message = driver.FindElement(By.XPath("//*[@id='right']/div/span"));
            IWebElement Message = driver.FindElement(By.XPath("//div[@class='chatBoxWrapper']//div[7]//div[1]//div[1]//span[1]"));
            string ActualMessage = Message.Text;
            //Match the message string
            Assert.AreEqual(ExpectedMsg, ActualMessage);

        }

       
    }
}
