namespace ChatPulse.BusinessLogic
{
    public class ObsAuthenticationInfo
    {
        public string Challenge { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
    }
}