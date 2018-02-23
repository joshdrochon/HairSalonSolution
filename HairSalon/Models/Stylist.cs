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

    public Stylist(int Id, string Name, string Email, string StartDate)
    {
      this._id = Id;
      this._name = Name;
      this._email = Email;
      this._startdate = StartDate;
    }

    //_id getter/setter
    public int GetId()
    {
      return _id = 0;
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

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylist;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistEmail = rdr.GetString(2);
        string stylistStartDate = rdr.GetString(3);
        Stylist newStylist = new Stylist
        (stylistId, stylistName, stylistEmail, stylistStartDate);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

  }


}
