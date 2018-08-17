using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ConsoleApp1
{
    class User : RelationalModel
    {

        public User(MySqlConnection con) : base(con)
        {
            this.timestamps = false;
        }

    }
}
