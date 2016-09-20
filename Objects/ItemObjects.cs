using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.Objects
{
  public class Item
  {
    //Properties
    private string _description;
    private int _id;
    // Contstructor
    public Item(string description, int id = 0)
    {
      _description = description;
      _id = id;
    }
    //Getters
    public string GetDescription()
    {
      return _description;
    }
    public int GetID()
    {
      return _id;
    }
    //Additional Setters
    public void SetDescription(string description)
    {
      _description = description;
    }
    public void SetId(int id)
    {
      _id = id;
    }

    //Static Methods
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      string command = "INSERT INTO tasks (description) OUTPUT INSERTED.id VALUES (@Desription);";
      SqlCommand cmd = new SqlCommand(command, conn);

      SqlParameter dp = new SqlParameter();
      dp.ParameterName = "@Desription";
      dp.Value = this.GetDescription();
      cmd.Parameters.Add(dp);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read() )
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM tasks;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public static List<Item> GetAll()
    {
      List<Item> allItem = new List<Item>{};

      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM tasks;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int ItemId = rdr.GetInt32(0);
        string ItemDescription = rdr.GetString(1);
        Item newTask = new Item(ItemDescription, ItemId);
        allItem.Add(newTask);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allItem;
    }
    //Overrides
    public override bool Equals(System.Object otherTask)
    {
      if (!(otherTask is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherTask;
        bool descriptionEquality = ( this.GetDescription() == newItem.GetDescription() );
        return (descriptionEquality);
      }
    }
  }
}
