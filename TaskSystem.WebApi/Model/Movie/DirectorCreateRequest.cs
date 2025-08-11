using System;

namespace TaskSystem.WebApi.Model.Movie
{
    public class DirectorCreateRequest
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }
    }
}
