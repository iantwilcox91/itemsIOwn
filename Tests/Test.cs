using System;
using Xunit;
using Inventory.Objects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Inventory;

namespace Testing
{
  public class Tests : IDisposable
  {
    public Tests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=items_tests;Integrated Security=SSPI;";
    }
    [Fact]
    public void Item_TestingCreatorMethod_true()
    {
      Item newItem = new Item("My Computer");
      Assert.Equal("My Computer", newItem.GetDescription() );
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      Item newItem = new Item("Computer");

      newItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testCase = new List<Item> {newItem};

      Assert.Equal(testCase, result);
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Item.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      Item.DeleteAll();
    }
  }
}
