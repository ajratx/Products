namespace Products.DAL.File.JSON
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    using Products.DAL.File.Interfaces;

    public sealed class JsonSerializer<T> : BaseSerializer<T>
    {
        private readonly JavaScriptSerializer serializer;

        public JsonSerializer(ISerializerSettings settings)
            : base(settings) =>
            serializer = new JavaScriptSerializer();

        public override async Task SerializeAsync(IEnumerable<T> objects)
        {
            if (File.Exists(Settings.FilePath))
            {
                var objectFromFile = await DeserializeAsync().ConfigureAwait(false);
                objects = objects.Union(objectFromFile);
            }

            var jsonFileBytes = Encoding.UTF8.GetBytes(serializer.Serialize(objects));
            using (var jsonFileStream = new FileStream(Settings.FilePath, FileMode.Create))
            {
                await jsonFileStream.WriteAsync(
                        jsonFileBytes,
                        0,
                        jsonFileBytes.Length,
                        CancellationToken.None)
                    .ConfigureAwait(false);
            }
        }

        public override async Task<IEnumerable<T>> DeserializeAsync()
        {
            return await Task.Run(
                       () =>
                           {
                               var json = File.ReadAllText(Settings.FilePath, Encoding.UTF8);
                               return serializer.Deserialize<IEnumerable<T>>(json);
                           }).ConfigureAwait(false);

        }
    }
}
