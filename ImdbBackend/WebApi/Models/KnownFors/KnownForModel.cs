﻿using System.Text.Json.Serialization;
using WebApi.Models.Titles;

namespace WebApi.Models.KnownFors
{
    public class KnownForModel
    {
        public string Url { get; set; }
        public string PrimaryName { get; set; } = string.Empty;


    }
}
