﻿public class conection{
    //private static string _cnn = @"Data Source= DESKTOP-D4E3SAN\SQLSERVER2022; Initial Catalog=montevideoTerminal; Integrated Security=true";
    private static string _cnn = @"Data Source=montevideoTerminal.mssql.somee.com;Initial Catalog=montevideoTerminal;User ID=SebaMachado01_SQLLogin_1;Password=xuam6q5iy7;Packet Size=4096;TrustServerCertificate=True";
    public static string Cnn{
        get{ return _cnn; 
        }
    }
}
