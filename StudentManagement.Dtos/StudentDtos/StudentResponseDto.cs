using System;

namespace StudentManagement.Dtos.StudentDtos
{
    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FavouriteQuote { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; }
    }
}
