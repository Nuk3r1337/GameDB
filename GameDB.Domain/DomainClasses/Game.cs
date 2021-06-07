using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDB.Domain.DomainClasses
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? Release_Date { get; set; }
        public List<Genre> Genres { get; set; }
        public Publisher Publishers_Id { get; set; }
        public AgeRating Rating_Id { get; set; }
        public string Picture { get; set; }
        public List<Comment> Comments { get; set; }
        public List<User_Rating> User_Ratings { get; set; }
    }
    public class User_Rating
    {
        public int Id { get; set; }
        public int Rating { get; set; }
    }
    public class Barcode
    {
        public string Code { get; set; }
        public Game Games_id { get; set; }
    }
    public class InsertBarcode
    {
        public string Code { get; set; }
        public int Games_id { get; set; }
    }
    public class ExternalGameList
    {
        public List<ExternalGame> Items { get; set; }
    }
    public class ExternalGame
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AgeRating
    {
        public int Id { get; set; }
        public string Age_Rating { get; set; }
    }
    public class Genre
    {
        public int Id { get; set; }
        public string Genre_Name { get; set; }
    }
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string User { get; set; }
        public int Game { get; set; }
    }
}
