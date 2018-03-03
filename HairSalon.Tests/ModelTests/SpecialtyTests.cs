using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalonProject.Models;
using HairSalonProject;
using System;

namespace HairSalonProject.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
      Specialty.DeleteAll();
      /* we can omit Client.DeleteAll() on account of no
      association between Specialties and Clients */
    }

    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString =
      "server=localhost;user id=root;password=root;port=8889;database=josh_rochon_test;";
    }

    [TestMethod]
    public void GetAll_AllDatabaseEmptyAtFirst_0()
    {
      //Arrange
      int result = Specialty.GetAll().Count;
      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesSpecialtyToDataBase_Specialty()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Title", "Description...");
      testSpecialty.Save();
      //Act (Where we set values to the elements to be compared)
      int actualResult = Specialty.GetAll().Count;
      int expectedResult = 1;
      //Assert
      Assert.AreEqual(actualResult, expectedResult);
    }

    [TestMethod]
    public void GetAll_GetsAllSpecialtiesFromDataBase_2()
    {
      //Arrange
      Specialty testSpecialty1 = new Specialty("Loctician", "Description...");
      Specialty testSpecialty2 = new Specialty("Long Hair", "Description...");
      testSpecialty1.Save();
      testSpecialty2.Save();
      //Act
      List<Specialty> actualResult = Specialty.GetAll();
      List<Specialty> expectedResult = new List<Specialty>
      {testSpecialty1, testSpecialty2};
      //Assert
      CollectionAssert.AreEqual(actualResult, expectedResult);
    }

    [TestMethod]
    public void DeleteAll_DeletesAllSpecialtiesFromDataBase_0()
    {
      //Arrange
      Specialty newSpecialty1 = new Specialty("Some Title", "Some description...");
      Specialty newSpecialty2 = new Specialty("Another Title", "Another description...");
      //Act
      Specialty.DeleteAll();
      int result = Specialty.GetAll().Count;
      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Find_AssignsIdToObjectId_Specialty()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("x", "y");
      testSpecialty.Save();
      //Act
      Specialty savedSpecialty = Specialty.GetAll()[0];
      int result = savedSpecialty.GetId();
      int testId = testSpecialty.GetId();
      //Assert
      Assert.AreEqual(result, testId);
    }



  }
}
