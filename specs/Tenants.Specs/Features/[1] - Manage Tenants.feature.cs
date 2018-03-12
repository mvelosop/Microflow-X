﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.0.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Tenants.Specs.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class Feature_1_ManageTenantsFeature : Xunit.IClassFixture<Feature_1_ManageTenantsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "[1] - Manage Tenants.feature"
#line hidden
        
        public Feature_1_ManageTenantsFeature(Feature_1_ManageTenantsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Feature - [1] - Manage Tenants", "    As a service manager\r\n    I need to manage tenants\r\n    To keep control of th" +
                    "e service", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Scenario - 1.1 - Add tenants")]
        [Xunit.TraitAttribute("FeatureTitle", "Feature - [1] - Manage Tenants")]
        [Xunit.TraitAttribute("Description", "Scenario - 1.1 - Add tenants")]
        public virtual void Scenario_1_1_AddTenants()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scenario - 1.1 - Add tenants", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email"});
            table1.AddRow(new string[] {
                        "tenant-a@server.com"});
            table1.AddRow(new string[] {
                        "tenant-b@server.com"});
#line 8
    testRunner.Given("these tenants don\'t exist:", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Name"});
            table2.AddRow(new string[] {
                        "tenant-a@server.com",
                        "Add Tenant A"});
            table2.AddRow(new string[] {
                        "tenant-b@server.com",
                        "Add Tenant B"});
#line 13
    testRunner.When("I add tenants:", ((string)(null)), table2, "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Name"});
            table3.AddRow(new string[] {
                        "tenant-a@server.com",
                        "Add Tenant A"});
            table3.AddRow(new string[] {
                        "tenant-b@server.com",
                        "Add Tenant B"});
#line 18
    testRunner.Then("when querying for \"Add\" tenants I get these:", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute(DisplayName="Scenario - 1.2 - Avoid duplicate tenant name")]
        [Xunit.TraitAttribute("FeatureTitle", "Feature - [1] - Manage Tenants")]
        [Xunit.TraitAttribute("Description", "Scenario - 1.2 - Avoid duplicate tenant name")]
        [Xunit.InlineDataAttribute("tenant-c@server.com", "Unique Tenant C", new string[0])]
        [Xunit.InlineDataAttribute("tenant-d@server.com", "Unique Tenant D", new string[0])]
        public virtual void Scenario_1_2_AvoidDuplicateTenantName(string email, string name, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scenario - 1.2 - Avoid duplicate tenant name", exampleTags);
#line 23
this.ScenarioSetup(scenarioInfo);
#line 25
    testRunner.Given(string.Format("tenant \"{0}\" does not exist", email), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 26
    testRunner.When(string.Format("I add tenant \"{0}\", \"{1}\"", email, name), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
    testRunner.Then(string.Format("I can\'t add another tenant \"{0}\", \"{1}\"", email, name), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Scenario - 1.3 - Modify tenants")]
        [Xunit.TraitAttribute("FeatureTitle", "Feature - [1] - Manage Tenants")]
        [Xunit.TraitAttribute("Description", "Scenario - 1.3 - Modify tenants")]
        public virtual void Scenario_1_3_ModifyTenants()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scenario - 1.3 - Modify tenants", ((string[])(null)));
#line 34
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email"});
            table4.AddRow(new string[] {
                        "tenant-e@server.com"});
            table4.AddRow(new string[] {
                        "tenant-f@server.com"});
            table4.AddRow(new string[] {
                        "tenant-e-modified@server.com"});
            table4.AddRow(new string[] {
                        "tenant-f-modified@server.com"});
#line 36
    testRunner.Given("these tenants don\'t exist:", ((string)(null)), table4, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Name"});
            table5.AddRow(new string[] {
                        "tenant-e@server.com",
                        "Insert Tenant E"});
            table5.AddRow(new string[] {
                        "tenant-f@server.com",
                        "Insert Tenant F"});
#line 44
    testRunner.And("I add tenants:", ((string)(null)), table5, "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "FindEmail",
                        "Email",
                        "Name"});
            table6.AddRow(new string[] {
                        "tenant-e@server.com",
                        "tenant-e-modified@server.com",
                        "Modified Tenant E"});
            table6.AddRow(new string[] {
                        "tenant-f@server.com",
                        "tenant-f-modified@server.com",
                        "Modified Tenant F"});
#line 49
    testRunner.When("I modify the tenants like so:", ((string)(null)), table6, "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Name"});
            table7.AddRow(new string[] {
                        "tenant-e-modified@server.com",
                        "Modified Tenant E"});
            table7.AddRow(new string[] {
                        "tenant-f-modified@server.com",
                        "Modified Tenant F"});
#line 54
    testRunner.Then("when querying for \"Modified\" tenants I get these:", ((string)(null)), table7, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Scenario - 1.4 - Avoid duplicate email when modifying tenant")]
        [Xunit.TraitAttribute("FeatureTitle", "Feature - [1] - Manage Tenants")]
        [Xunit.TraitAttribute("Description", "Scenario - 1.4 - Avoid duplicate email when modifying tenant")]
        public virtual void Scenario_1_4_AvoidDuplicateEmailWhenModifyingTenant()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scenario - 1.4 - Avoid duplicate email when modifying tenant", ((string[])(null)));
#line 60
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email"});
            table8.AddRow(new string[] {
                        "tenant-g@server.com"});
            table8.AddRow(new string[] {
                        "tenant-h@server.com"});
#line 62
    testRunner.Given("these tenants don\'t exist:", ((string)(null)), table8, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Name"});
            table9.AddRow(new string[] {
                        "tenant-g@server.com",
                        "Insert Tenant G"});
            table9.AddRow(new string[] {
                        "tenant-h@server.com",
                        "Insert Tenant H"});
#line 68
    testRunner.And("I add tenants:", ((string)(null)), table9, "And ");
#line 73
    testRunner.Then("I get error \"DuplicateByEmailError\" when trying to modify tenant\'s email from \"te" +
                    "nant-g@server.com\" to \"tenant-h@server.com\":", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Scenario - 1.5 - Remove tenant")]
        [Xunit.TraitAttribute("FeatureTitle", "Feature - [1] - Manage Tenants")]
        [Xunit.TraitAttribute("Description", "Scenario - 1.5 - Remove tenant")]
        public virtual void Scenario_1_5_RemoveTenant()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scenario - 1.5 - Remove tenant", ((string[])(null)));
#line 76
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email"});
            table10.AddRow(new string[] {
                        "tenant-i@server.com"});
            table10.AddRow(new string[] {
                        "tenant-j@server.com"});
#line 78
    testRunner.Given("these tenants don\'t exist:", ((string)(null)), table10, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Name"});
            table11.AddRow(new string[] {
                        "tenant-i@server.com",
                        "Removed Tenant I"});
            table11.AddRow(new string[] {
                        "tenant-j@server.com",
                        "Removed Tenant J"});
#line 84
    testRunner.And("I add tenants:", ((string)(null)), table11, "And ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "FindEmail"});
            table12.AddRow(new string[] {
                        "tenant-j@server.com"});
#line 89
    testRunner.When("I remove these tenants:", ((string)(null)), table12, "When ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Name"});
            table13.AddRow(new string[] {
                        "tenant-i@server.com",
                        "Removed Tenant I"});
#line 93
    testRunner.Then("when querying for \"Removed\" tenants I get these:", ((string)(null)), table13, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                Feature_1_ManageTenantsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                Feature_1_ManageTenantsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
