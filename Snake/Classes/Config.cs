namespace Snake.Classes
{
    public class Config
    {
        private const string AppName = "Snake";
        private const string AppVersion = "V. 0.0.1";

        public readonly static string TitleAppNameAndVersion = $"{AppName} {AppVersion}";
        public readonly static string TitleErrorAppNameAndVersion = $"{TitleAppNameAndVersion} - Error";
    }
}
