namespace BLL.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public UserSignInInfoDTO SignInInfo { get; set; }
        public FullNameDTO UserFullName { get; set; }
    }
}