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
    public void Get_AllDatabaseEmptyAtFirst_0()
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


  }
}
