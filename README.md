# RelationalModel for C#

A simple Object-relational Mapping for C#

### Examples 

Change the namespace from **ConsoleApp1** to your current namespace

1. Create a class that extends RelationalModel

```c#
// User.cs
...
class User : RelationalModel {

}
```

2. In the super constructor you can define your properties for the model, by example the timestamps can be changed to false if you don't want to store timestamp changes in your table  
_More properties comming up to next updates :)_
```c#
// User.cs
...
public User(MySqlConnection con) : base(con) {
  this.timestamps = false;
}
```
Available Parameters:
  * timestamps - Default value: true - Update timestamps column with the current time in milliseconds (Unix Timestamp)
  * table - - Default value: Child class name in lower case + "s"

3. Initialize you MySQL Connection. Remember to import **MySQL.Data.MySqlClient**
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

foreach (Dictionary<string, object> rows in model.where(conditions))
{
    object value;
    if (rows.TryGetValue("name", out value)) {
        Console.WriteLine(value);
    }
}
```

6. Find with id (Integer)
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
    {"user", "foi" },
    {"senha", "456" }
};
model.insert(row);
```

## Authors

* **M. Pedroni** - [Pedr0ni](https://twitter.com/pedr0ni_)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
