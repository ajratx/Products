namespace Products.DAL.File.JSON
{
    using System.Collections.Generic;
    using System.IO;    
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    using Products.DAL.File.Interfaces;

    public sealed class JsonSerializer<T> : BaseSerializer<T>
    {
        private readonly JavaScriptSerializer serializer;

        public JsonSerializer(ISerializerSettings settings)
            : base(settings)
            => serializer = new JavaScriptSerializer();

        protected override async Task InternalSerializeAsync(IEnumerable<T> items)
        {   
            var jsonFileBytes = Encoding.UTF8.GetBytes(serializer.Serialize(items));
            using (var jsonFileStream = new FileStream(Settings.FilePath, FileMode.Create))
            {
                await jsonFileStream.WriteAsync(
                        jsonFileBytes,
                        0,
                        jsonFileBytes.Length,
                        CancellationToken.None)
                    .ConfigureAwait(true);
            }
        }

        protected override async Task<IEnumerable<T>> InternalDeserializeAsync()
        {
            return await Task.Run(
                       () =>
                           {
                               if (!File.Exists(Settings.FilePath)) return null;

                               var json = File.ReadAllText(Settings.FilePath, Encoding.UTF8);
                               return serializer.Deserialize<IEnumerable<T>>(json);
                           }).ConfigureAwait(true);
        }
    }
}
