using System;

namespace TaskSystem.WebApi.Model.Movie
{
    public class DirectorRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }
    }
}
