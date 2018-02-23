using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalonProject;
using System;

namespace HairSalonProject.Models
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private string _email;
    private string _startdate;

    public Stylist(string Name, string Email, string StartDate, int id = 0)
    {
      this._id = Id;
      this._name = Name;
      this._email = Email;
      this._startdate = StartDate;
    }

    //_id getter/setter
    public int GetId()
    {
      return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }

    //_name getter/setter
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    //_email getter/setter
    public string GetEmail()
    {
      return _email;
    }
    public void SetEmail(string newEmail)
    {
      _email = newEmail;
    }

    //_startdate getter/setter
    public string GetStartDate()
    {
      return _startdate;
    }
    public void SetStartDate(string newStartDate)
    {
      _startdate = newStartDate;
    }


  }


}
