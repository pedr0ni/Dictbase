# Dictbase for C#

Dictionary-based ORM made in .NET

### Examples 

Change the namespace from **Dictbase** to your current namespace

1. Create a class that extends Dictbase

```c#
// User.cs
...
class User : Dictbase {

}
```

2. In the super constructor you can define your properties for the model, by example the timestamps can be changed to false if you don't want to store timestamp changes in your table
```c#
// User.cs
...
public User(MySqlConnection con) : base(con) {
  this.timestamps = false;
}
```
Available Parameters:
  * timestamps - Default value: true - Update timestamps (created and updated) column with the current time in milliseconds (Unix Timestamp)
  * table - - Default value: Child class name in lower case + "s"  
_More properties comming up to next updates :)_

3. Initialize you MySQL Connection. Remember to import **MySql.Data.MySqlClient**
```c#
// Program.cs

MySqlConnection con = new MySqlConnection("Server=YOUR_SERVER;Database=YOUR_DB;Uid=YOUR_UID;Pwd=YOUR_PWD;SslMode=none;");
con.Open();

```

4. Create a instance of your model class **not the superclass** passing the MySqlConnection to the constructor
```c#
User model = new User(con);
```

5. SELECT WHERE
```c#
Dictionary<string, string> conditions = new Dictionary<string, string>
{
    {"name", "Matheus"},
    {"active", "1"}
};

/*
 * The string represents the column name in the table
 * The object represents the value contained in the column
 */
foreach (Dictionary<string, object> rows in model.where(conditions))
{
    object value;
    if (rows.TryGetValue("name", out value)) {
        Console.WriteLine(value);
    }
}
```

6. Find by id - Returns only one row
```c#
Dictionary<string, object> row = model.find(1);
object value;
if (row.TryGetValue("name", out value))
{
    Console.WriteLine(value);
}
```

7. INSERT INTO
```c#
Dictionary<string, object> row = new Dictionary<string, object>
{
    {"name", "matheus" },
    {"senha", "456" }
};
model.insert(row);
```

8. UPDATE
```c#
model.update(new Dictionary<string, object>
{
    {"senha", "123456" }
}, new Dictionary<string, string>
{
    {"name", "matheus" }
});
```

9. SELECT ALL
```c#
foreach (Dictionary<string, object> values in model.all())
{
    object value;
    if (values.TryGetValue("name", out value))
    {
        Console.WriteLine(value);
    }
}
```

## Authors

* **M. Pedroni** - [Pedr0ni](https://twitter.com/pedr0ni_)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
