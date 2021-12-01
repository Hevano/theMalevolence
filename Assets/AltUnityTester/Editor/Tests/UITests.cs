using NUnit.Framework;
using Altom.AltUnityDriver;
using System.Collections.Generic;

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

    [Test]
    public void DraftingTest(){
        altUnityDriver.LoadScene("BossOne");
        altUnityDriver.LoadScene("DeckBuilder");
        //Get all card displays in 
        var cards = altUnityDriver.FindObjects(By.COMPONENT, "Transform");
        List<AltUnityObject> draftCards = new List<AltUnityObject>();
        foreach(AltUnityObject card in cards){
            if(card.GetComponentProperty("Transform", "parent") == "Draft Display"){
                draftCards.Add(card);
            }
        }
        if(draftCards.Count == 0){
            Assert.Pass();
        }
        try {
            draftCards[0].Click();
            draftCards[1].Click();
            draftCards[2].Click();
        } catch(System.Exception e){
            Assert.Fail();
        }
        
        
        var confirmDraftButton = altUnityDriver.FindObject(By.NAME, "EndDraftButton");
        confirmDraftButton.Click();
        var continueButton = altUnityDriver.FindObject(By.NAME, "ExitButton");
        continueButton.Click();
        altUnityDriver.WaitForCurrentSceneToBe("BossHeadmaster");
    }

    [Test]
    public void AttackTest(){

    }

    [Test]
    public void PlayCardTest(){
        
    }

}