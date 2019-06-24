namespace FoxOffice
{
    public class AppSettings
    {
        public AppSettings(
            string storageConnectionString,
            string messageQueueName,
            string eventStoreTableName,
            string mementStoreBlobName,
            string cosmosDbEndpoint,
            string cosmosDbAuthKey,
            string readModelDatabaseId,
            string readModelCollectionId)
        {
            StorageConnectionString = storageConnectionString;
            MessageQueueName = messageQueueName;
            EventStoreTableName = eventStoreTableName;
            MementoStoreBlobName = mementStoreBlobName;
            CosmosDbEndpoint = cosmosDbEndpoint;
            CosmosDbAuthKey = cosmosDbAuthKey;
            ReadModelDatabaseId = readModelDatabaseId;
            ReadModelCollectionId = readModelCollectionId;
        }

        public string StorageConnectionString { get; }

        public string MessageQueueName { get; }

        public string EventStoreTableName { get; }

        public string MementoStoreBlobName { get; set; }

        public string CosmosDbEndpoint { get; }

        public string CosmosDbAuthKey { get; }

        public string ReadModelDatabaseId { get; }

        public string ReadModelCollectionId { get; }
    }
}
