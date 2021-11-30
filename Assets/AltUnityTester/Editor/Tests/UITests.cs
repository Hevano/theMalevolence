using NUnit.Framework;
using Altom.AltUnityDriver;

public class UITests
{
    public AltUnityDriver altUnityDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altUnityDriver =new AltUnityDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altUnityDriver.Stop();
    }

    [Test]
    public void EndPhaseTest()
    {
	//Here you can write the test
        altUnityDriver.LoadScene("BossOne");
        altUnityDriver.FindObject(By.NAME, "EndPhaseButton").Click();
        //var gm = altUnityDriver.FindObject(By.NAME, "Gample")
        if(GameManager.manager.phase == Enums.GameplayPhase.Resolve){
            Assert.Pass();
        } else {
            Assert.Fail();
        }
    }

}