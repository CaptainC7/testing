using System.ComponentModel.DataAnnotations;

namespace testing.Models
{
    public class UserDTO
    {
        //[Required]
        public string FName { get; set; } = "";
        //[Required]
        public string LName { get; set; } = "";
        //[Required]
        public string Gender { get; set; } = "";
        //[Required]
        public string BDate { get; set; } = "";
        //[Required]
        public string Username { get; set; } = "";
        //[Required]
        public string Password { get; set; } = "";
    }
}
