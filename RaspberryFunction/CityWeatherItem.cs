using Azure;
using Azure.Data.Tables;

namespace RaspberryFunction
{
    internal class CityWeatherItem : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string Weather { get; set; }
    }
}
