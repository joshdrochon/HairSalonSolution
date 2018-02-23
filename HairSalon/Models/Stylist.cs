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

    public Stylist(string Name, string Email, string StartDate, int Id=0)
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


    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        //every test checking for equality must be put here...
        return (idEquality);
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      // var cmd = conn.CreateCommand() as MySqlCommand;
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
        (stylistName, stylistEmail, stylistStartDate, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }


    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;

      //_id
      cmd.CommandText = @"INSERT INTO stylist (id)
      VALUES (@stylistId);";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@stylistId";
      id.Value = this._id;
      cmd.Parameters.Add(id);

      //_name
      cmd.CommandText = @"INSERT INTO stylist (name)
      VALUES (@stylistName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@stylistName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      //_email
      cmd.CommandText = @"INSERT INTO stylist (email)
      VALUES (@stylistEmail);";

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@stylistEmail";
      email.Value = this._email;
      cmd.Parameters.Add(email);

      //_startdate
      cmd.CommandText = @"INSERT INTO stylist (startdate)
      VALUES (@stylistStartDate);";

      MySqlParameter startdate = new MySqlParameter();
      startdate.ParameterName = "@stylistStartDate";
      startdate.Value = this._startdate;
      cmd.Parameters.Add(startdate);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }


  }

}
