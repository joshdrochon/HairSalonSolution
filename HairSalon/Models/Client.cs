using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalonProject;
using System;

namespace HairSalonProject.Models
{
  public class Client
  {
    private int _id;
    private string _name;
    private string _email;
    private string _firstappt;

    private int _stylistId;

    public Client(string Name, string Email, string FirstAppt, int StylistId=0, int Id=0)
    {
      this._id = Id;
      this._name = Name;
      this._email = Email;
      this._firstappt = FirstAppt;

      this._stylistId = StylistId;
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
    public string GetFirstAppt()
    {
      return _firstappt;
    }
    public void SetFirstAppt(string newFirstAppt)
    {
      _firstappt = newFirstAppt;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }


    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();

        return (idEquality);

      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM client;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        string clientEmail = rdr.GetString(2);
        string clientFirstAppt = rdr.GetString(3);
        int clientStylistId = rdr.GetInt32(4);

        Client newClient = new Client
        (clientName, clientEmail, clientFirstAppt, clientStylistId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"INSERT INTO client (id, name, email, firstappt, stylist_id)
      VALUES (@clientId, @clientName, @clientEmail, @clientFirstAppt, @stylist_id);";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@clientId";
      id.Value = this._id;
      cmd.Parameters.Add(id);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@clientName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@clientEmail";
      email.Value = this._email;
      cmd.Parameters.Add(email);

      MySqlParameter firstappt = new MySqlParameter();
      firstappt.ParameterName = "@clientFirstAppt";
      firstappt.Value = this._firstappt;
      cmd.Parameters.Add(firstappt);

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@stylist_id";
      stylist_id.Value = this._stylistId;
      cmd.Parameters.Add(stylist_id);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM client;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM client WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int clientId = 0;
      string clientName = "";
      string clientEmail = "";
      string clientFirstAppt = "";
      int clientStylistId = 0;

      while(rdr.Read())
      {
        //arguments in rdr methods correspond to index of the table rows
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        clientEmail = rdr.GetString(2);
        clientFirstAppt = rdr.GetString(3);
        clientStylistId = rdr.GetInt32(4);
      }

      Client foundClient = new Client
      (clientName, clientEmail, clientFirstAppt, clientStylistId, clientId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

      return foundClient;
    }

  }
}

//note, if non-static we must preface instance with class name to target it
