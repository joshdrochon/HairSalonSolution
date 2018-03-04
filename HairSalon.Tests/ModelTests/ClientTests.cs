using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalonProject.Models;
using HairSalonProject;
using System;

namespace HairSalonProject.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString =
      "server=localhost;user id=root;password=root;port=8889;database=josh_rochon_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;
      Console.WriteLine("In line 29 in ClientTests there are " + result + " in the database.");
      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Delete_DeletesIndividualClientFromDataBase()
    {
      //Arrange
      Client testClient1 = new Client("x", "y", "z");
      testClient1.Save();
      Client testClient2 = new Client("m", "v", "c");
      testClient2.Save();

      //Act
      testClient2.Delete(testClient2.GetId());

      int actualResult = Client.GetAll().Count;
      int expectedResult = 1;

      //Assert
      Assert.AreEqual(actualResult, expectedResult);
    }

    [TestMethod]
    public void DeleteAll_DeletesAllClientsFromDataBase_0()
    {
      //Arrange
      Client newClient1 = new Client("x", "y", "z");
      Client newClient2 = new Client("a", "b", "c");
      //Act
      Client.DeleteAll();
      int result = Client.GetAll().Count;
      //Assert
      Assert.AreEqual(0, result);
    }


    [TestMethod]
    public void Save_SavesClientToDataBase_Client()
    {
      //Arrange
      Client testClient = new Client
      ("Kilo Ren", "Kilo17@gmail.com", "06/12/12", 1);
      int expectedResult = 1; //one client expected in database
      //Act
      testClient.Save();
      int actualResult = Client.GetAll().Count;
      //Assert
      Assert.AreEqual(expectedResult, actualResult);
      Console.WriteLine(expectedResult);
      Console.WriteLine(actualResult);
    }

    [TestMethod]
    public void GetAll_GetsAllClientsFromDataBase_2()
    {
      //Arrange
      Client testClient1 = new Client("Name", "Email", "FirstAppt");
      Client testClient2 = new Client("Name", "Email", "FirstAppt");
      testClient1.Save();
      testClient2.Save();
      //Act
      List<Client> actualResult = Client.GetAll();
      List<Client> expectedResult = new List<Client>
      {testClient1, testClient2};
      //Assert
      CollectionAssert.AreEqual(actualResult, expectedResult);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client
      ("Han Solo", "Solo@gmail.com", "03/17/77", 1);

      //Act
      testClient.Save();
      /* GetAll returns a list, therefore index 0 is the
      first element of the list, with an id */
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(result, testId);
    }

    [TestMethod]
    public void Find_FindsClientInDatabase_Client()
    {
      //Arrange
      Client testClient = new Client
      ("Reese WitherFork", "ForkR@gmail.com", "02/29/2000", 1);
      testClient.Save();
      //Act
      Client foundClient = Client.Find(testClient.GetId());
      //Assert
      Console.WriteLine("TEST CLIENT ID IS: " + testClient.GetId());
      Console.WriteLine("FOUND CLIENT IS IS: " + foundClient.GetId());

      Assert.AreEqual(testClient, foundClient);
    }

  }
}
