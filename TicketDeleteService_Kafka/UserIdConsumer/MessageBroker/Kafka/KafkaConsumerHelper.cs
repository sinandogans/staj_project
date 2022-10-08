﻿using Confluent.Kafka;
using System.Text.Json;

namespace Worker.MessageBroker.Kafka
{
    public class KafkaConsumerHelper
    {
        private readonly IConfiguration _configuration;
        private IConsumer<Ignore, string> _consumer;
        private ConsumeResult<Ignore, string> _lastMessage;

        public KafkaConsumerHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T? ConsumeMessage<T>(string topic)
        {
            _consumer.Subscribe(topic);
            var result = _consumer.Consume(2000);
            _lastMessage = result;
            //_consumer.Close();
            if (result == null)
                return default;
            return JsonSerializer.Deserialize<T>(result.Message.Value);


            //config değeri var değer kadar mesaj dönecek ConsumeMessage metodu
            // mesaj sayısına ulaşırsa mesajları dönecek
            // configde süre var, önce süre dolarsa da aldığı mesajları dönecek
        }

        public void Connect(string groupId)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false

            };
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        public void Commit()
        {
            _consumer.Commit(_lastMessage);
        }
    }
}
