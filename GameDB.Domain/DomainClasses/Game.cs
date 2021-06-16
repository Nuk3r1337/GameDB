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
        public DateTimeOffset? Release_date { get; set; }
        public List<Genre> Game_genre { get; set; }
        public Publisher Publisher { get; set; }
        public AgeRating Age_rating { get; set; }
        public string Picture { get; set; }
        public List<Comment> Comments { get; set; }
        public List<User_Rating> User_Ratings { get; set; }
    }
    public class Game_Has_Genre
    {
        public int Games_id { get; set; }
        public int Genre_id { get; set; }
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
        public List<Game> game { get; set; }
    }
    public class AgeRating
    {
        public int Id { get; set; }
        public string Age_rating { get; set; }
        public List<Game> game { get; set; }
    }
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GenreName genre { get; set; }
        public List<Game> game { get; set; }
    }
    public class GenreName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Comment
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public int users_id { get; set; }
        public int games_id { get; set; }
    }
    public class GameEdit
    {
        public Game game { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<AgeRating> AgeRatings { get; set; }
    }
}
