using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalonProject.Models;
using HairSalonProject;
using System;

namespace HairSalonProject.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }

    public StylistTests()
    {
      DBConfiguration.ConnectionString =
      "server=localhost;user id=root;password=root;port=8889;database=josh_rochon_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;
      Console.WriteLine("Line 29 " + result);
      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist
      ("Yoda", "Yoda@gmail.com", "03/14/18");

      //Act
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(result, testId);
    }


    [TestMethod]
    public void Save_SavesToDataBase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist
      ("Luke Skywalker", "Skywalker@gmail.com", "03/14/17");
      int expectedResult = 1;
      //Act
      testStylist.Save();
      int actualResult = Stylist.GetAll().Count;
      //Assert
      Assert.AreEqual(expectedResult, actualResult);
      Console.WriteLine(expectedResult);
      Console.WriteLine(actualResult);
    }

    [TestMethod]
    public void Find_FindsStylestInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist
      ("Jack Sparrow", "SparrowMe@gmail.com", "04/12/1999");
      testStylist.Save();
      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());
      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetClients_GetsAllClientsWithStylist_ClientList()
    {
      //create a Stylist object
      Stylist testStylist = new Stylist
      ("Name", "Email", "StartDate");
      testStylist.Save();

      //create two Client objects and add them to the testStylist via GetId()
      Client firstClient = new Client("Bob", "b1952@uw.edu", "12/25/2049", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Sam", "Samwise@gmail.com", "N/A", testStylist.GetId());
      secondClient.Save();
      //add both Client objects to a list
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      //call GetClient() method on teststylist set equal to a list
      List<Client> resultClientList = testStylist.GetClients();
      //assert both lists are equal and contain 2 client objects
      // CollectionAssert.AreEqual(testClientList, resultClientList);
      Assert.AreEqual(resultClientList.Count, 2);

    }
  }
}
