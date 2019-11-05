using DotLiquid.Tags;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    
    class DraftTest: TestBase
    {      
        [Test]
        public void DraftVerification()
    {
        SignInAccount();
        GoToGmail();
        GoToDraftFolder();
        CreateDraft();
        UpdateDraft();
        SingOut();
    }
    
    }
}


