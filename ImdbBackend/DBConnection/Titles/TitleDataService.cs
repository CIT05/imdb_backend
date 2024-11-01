﻿using DataLayer.Titles;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Titles
{
    public class TitleDataService(string connectionString) : ITitleDataService
    {
        private readonly string _connectionString = connectionString;


        public List<Title> GetTitles(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.Titles.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public Title? GetTitleById(string tconst)
        {
           var db = new ImdbContext(_connectionString);
            return db.Titles.Where(title => title.TConst == tconst).Include(title => title.Rating).SingleOrDefault();
        }

        public int NumberOfTitles()
        {
            var db = new ImdbContext(_connectionString);
            return db.Titles.Count();
        }
    }

}
