namespace Bandit.NSI.Daemon.Configuration
{
    public class DaemonConfiguration
    {
        public const string ServiceName = "NSI";
        public DatabaseConfiguration TempDatabase { get; set; }
        public DatabaseConfiguration DecisDatabase { get; set; }
        public DatabaseConfiguration AuthDatabase { get; set; }
        public JWTConfiguration JWT { get; set; }
        public APIConfiguration API { get; set; }

    }
}
