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
      Specialty.DeleteAll();
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
    public void Delete_DeletesStylistAssociationsFromDatabase_StylistList()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Title", "Description");
      testSpecialty.Save();

      string testName = "Bill";
      string testEmail = "B52@gmail.com";
      string testStartDate = "12/12/2012";

      Stylist testStylist = new Stylist(testName, testEmail, testStartDate);
      testStylist.Save();

      //Act
      testStylist.AddSpecialty(testSpecialty);
      testStylist.Delete();

      List<Stylist> resultSpecialtyStylists = testSpecialty.GetStylists();
      List<Stylist> testSpecialtyStylists = new List<Stylist> {};

      //Assert
      CollectionAssert.AreEqual(testSpecialtyStylists, resultSpecialtyStylists);
    }

    [TestMethod]
    public void Delete_DeletesIndividualStylist_1()
    {
      Stylist newStylist1 = new Stylist("x", "y", "z");
      Stylist newStylist2 = new Stylist("a", "b", "c");
      newStylist1.Save();
      newStylist2.Save();

      int currentStylists = Stylist.GetAll().Count;
      Console.WriteLine("There are currently " + currentStylists + " stylists  in the database.");

      newStylist2.Delete();

      int updatedStylists = Stylist.GetAll().Count;
      Console.WriteLine("There is now " + updatedStylists + " stylist in the database.");

      int result = Stylist.GetAll().Count;

      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void DeleteAll_DeletesAllStylistsFromDataBase_0()
    {
      //Arrange
      Stylist newStylist1 = new Stylist("x", "y", "z");
      Stylist newStylist2 = new Stylist("a", "b", "c");
      //Act
      Stylist.DeleteAll();
      int result = Stylist.GetAll().Count;
      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Find_AssignsIdToObject_Id()
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
    public void GetAll_GetsAllStylistsFromDataBase_2()
    {
      //Arrange
      Stylist testStylist1 = new Stylist("x", "y", "z");
      Stylist testStylist2 = new Stylist("a", "b", "c");
      testStylist1.Save();
      testStylist2.Save();
      //Act
      List<Stylist> actualResult = Stylist.GetAll();
      List<Stylist> expectedResult = new List<Stylist>
      {testStylist1, testStylist2};
      //Assert
      CollectionAssert.AreEqual(actualResult, expectedResult);
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

    [TestMethod]
    public void AddSpecialty_AddsSpecialtyToStylist()
    {
      //Arrange
      Stylist testStylist = new Stylist
      ("Robert", "jones@uw.edu", "N/A");
      testStylist.Save();

      Specialty testSpecialty = new Specialty
      ("Loctician", "Specializes in dreadlocks.");
      testSpecialty.Save();

      Specialty testSpecialty2 = new Specialty
      ("Men", "Specializes in men haircuts.");
      testSpecialty2.Save();

      //Act
      testStylist.AddSpecialty(testSpecialty);
      testStylist.AddSpecialty(testSpecialty2);
      List<Specialty> testList = new List<Specialty>{testSpecialty, testSpecialty2};
      List<Specialty> result = testStylist.GetSpecialties();

      //Assert
      Assert.AreEqual(testList.Count, result.Count);
    }

    [TestMethod]
    public void GetSpecialties_ReturnsAllStylistSpecialties_SpecialtyList()
    {
      //Arrange
      Stylist testStylist = new Stylist
      ("John", "john@gmail.com", "03/14/1988");
      testStylist.Save();

      Specialty testSpecialty1 = new Specialty
      ("Hair Coloring", "Specializes in hair coloring of textures.");
      testSpecialty1.Save();

      Specialty testSpecialty2 = new Specialty
      ("Women", "Specializes in women's hair.");
      testSpecialty2.Save();
      //Act
      testStylist.AddSpecialty(testSpecialty1);
      testStylist.AddSpecialty(testSpecialty2);
      List<Specialty> testList = new List<Specialty> {testSpecialty1, testSpecialty2};
      List<Specialty> savedSpecialties = testStylist.GetSpecialties();
      //Assert
      Assert.AreEqual(testList.Count, savedSpecialties.Count);
    }

    [TestMethod]
    public void EditName_UpdatesNameInDatabase_String()
    {
      //Arrange
      string originalName = "James Bond";
      Stylist testStylist = new Stylist(originalName, "", "", 1);
      testStylist.Save();
      string updatedName = "Samwise the Brave";

      //Act
      testStylist.EditName(updatedName);

      string result = Stylist.Find(testStylist.GetId()).GetName();

      //Assert
      Assert.AreEqual(updatedName , result);
    }

  }
}
