namespace Products.Client.WPF.Core.Settings
{
    using Products.DAL.File.Interfaces;

    public class SerializerSettings : ISerializerSettings
    {
        public string FilePath { get; set; }
    }
}
