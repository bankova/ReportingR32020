using System;
using Bellatrix.MachineAutomation;
using NUnit.Framework;

namespace Bellatrix.Web.NUnit.Tests
{
    [SetUpFixture]
    public class TestsInitialize
    {
        public TestsInitialize()
        {
        }

        [OneTimeSetUp]
        public void AssemblyInitialize()
        {
            WebTest.App.UseUnityContainer();
            WebTest.App.UseExceptionLogger();
            WebTest.App.UseNUnitSettings();
            WebTest.App.UseControlDataHandlers();
            WebTest.App.UseBrowserBehavior();
            WebTest.App.UseLogExecutionBehavior();
            WebTest.App.UseControlLocalOverridesCleanBehavior();
            WebTest.App.UseFFmpegVideoRecorder();
            WebTest.App.UseFullPageScreenshotsOnFail();
            WebTest.App.UseLogger();
            WebTest.App.UseElementsBddLogging();
            WebTest.App.UseHighlightElements();
            WebTest.App.UseEnsureExtensionsBddLogging();
            WebTest.App.UseLayoutAssertionExtensionsBddLogging();
            WebTest.App.AssemblyInitialize();
            WebTest.App.Initialize();

            // Software machine automation module helps you to install the required software to the developer's machine
            // such as a specific version of the browsers, browser extensions, and any other required software.
            // You can configure it from BELLATRIX configuration file testFrameworkSettings.json
            //  "machineAutomationSettings": {
            //      "isEnabled": "true",
            //      "packagesToBeInstalled": [ "googlechrome", "firefox --version=65.0.2", "opera" ]
            //  }
            //
            // You need to specify the packages to be installed in the packagesToBeInstalled array. You can search for packages in the
            // public community repository- https://chocolatey.org/
            //
            // To use the service you need to start Visual Studio in Administrative Mode. The service supports currently only Windows.
            // In the future BELLATRIX releases we will support OSX and Linux as well.
            //
            // To use the machine automation setup- install Bellatrix.MachineAutomation NuGet package.
            // SoftwareAutomationService.InstallRequiredSoftware();
        }

        [OneTimeTearDown]
        public void AssemblyCleanUp()
        {
            WebTest.App.Dispose();
            WebTest.Screen?.Dispose();
        }
    }
}