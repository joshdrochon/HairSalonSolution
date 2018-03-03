using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalonProject;
using System;

namespace HairSalonProject.Models
{
  public class Specialty
  {
    private int _id;
    private string _title;
    private string _description;
    public Specialty(string Title, string Description, int Id=0)
    {
      this._id = Id;
      this._title = Title;
      this._description = Description;
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

    //_title getter/setter
    public string GetTitle()
    {
      return _title;
    }
    public void SetTitle(string newTitle)
    {
      _title = newTitle;
    }

    //_description getter/setter
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetId() == newSpecialty.GetId());

        return (idEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"INSERT INTO specialty (id, title, description)
      VALUES (@specialtyId, @specialtyTitle, @specialtyDescription);";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@specialtyId";
      id.Value = this._id;
      cmd.Parameters.Add(id);

      MySqlParameter title = new MySqlParameter();
      title.ParameterName = "@specialtyTitle";
      title.Value = this._title;
      cmd.Parameters.Add(title);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@specialtyDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialty;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyTitle = rdr.GetString(1);
        string specialtyDescription = rdr.GetString(2);

        Specialty newSpecialty = new Specialty
        (specialtyTitle, specialtyDescription, specialtyId);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand
      ("DELETE FROM specialty WHERE id = @SpecialtyId; DELETE FROM specialty_stylist WHERE specialty_id = @SpecialtyId;", conn);
      MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@SpecialtyId";
      specialtyIdParameter.Value = this.GetId();

      cmd.Parameters.Add(specialtyIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialty;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialty WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int specialtyId = 0;
      string specialtyTitle = "";
      string specialtyDescription = "";

      while(rdr.Read())
      {
        //arguments in rdr methods correspond to index of the table rows
        specialtyId = rdr.GetInt32(0);
        specialtyTitle = rdr.GetString(1);
        specialtyDescription = rdr.GetString(2);
      }

      Specialty foundSpecialty = new Specialty
      (specialtyTitle, specialtyDescription, specialtyId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

      return foundSpecialty;
    }

    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialty_stylist
      (specialty_id, stylist_id) VALUES (@SpecialtyId, @StylistId);";

      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@SpecialtyId";
      specialty_id.Value = this._id;
      cmd.Parameters.Add(specialty_id);

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@StylistId";
      stylist_id.Value = this._id;
      cmd.Parameters.Add(stylist_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylist_id FROM specialty_stylist WHERE specialty_id = @SpecialtyId;";

      MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@SpecialtyId";
      specialtyIdParameter.Value = _id;
      cmd.Parameters.Add(specialtyIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<int> stylistIds = new List<int> {};
      while(rdr.Read())
      {
          int stylistId = rdr.GetInt32(0);
          stylistIds.Add(stylistId);
      }
      rdr.Dispose();

      List<Stylist> stylists = new List<Stylist> {};
      foreach (int stylistId in stylistIds)
      {
        var stylistQuery = conn.CreateCommand() as MySqlCommand;
        stylistQuery.CommandText = @"SELECT * FROM stylist WHERE id = @StylistId;";

        MySqlParameter stylistIdParameter = new MySqlParameter();
        stylistIdParameter.ParameterName = "@StylistId";
        stylistIdParameter.Value = stylistId;
        stylistQuery.Parameters.Add(stylistIdParameter);

        var stylistQueryRdr = stylistQuery.ExecuteReader() as MySqlDataReader;
        while(stylistQueryRdr.Read())
        {
          int thisStylistId = stylistQueryRdr.GetInt32(0);
          string stylistName = stylistQueryRdr.GetString(1);
          string stylistEmail = stylistQueryRdr.GetString(2);
          string stylistStartDate = stylistQueryRdr.GetString(3);

          Stylist foundStylist = new Stylist(stylistName, stylistEmail, stylistStartDate, thisStylistId);
          stylists.Add(foundStylist);
        }

        stylistQueryRdr.Dispose();

      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return stylists;
    }
  }
}
