﻿namespace DataAccess.MongoDb.Models
{
    public class StajProjectDbSettings
    {
        public string ConnectionStrings { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
    }
}
