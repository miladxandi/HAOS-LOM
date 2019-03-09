namespace HAOS.Object
{
    public class Result
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Profile { get; set; }
        public string Token { get; set; }
    }

    public class RootObject
    {
        public string Status { get; set; }
        public Result Result { get; set; }
    }
}