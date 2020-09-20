# RabbitMQSample


This application has been developed for sample of queue logic.

.Net Core api getting request for credit card expense and do some db transactions on PostgreSQL. Then add queue(RabbitMQ) some information about expense and customer.

Node.js application is a consumer app and listening the queue. When a message added queue,consumer app receive it and send information mail to customer about credit card transaction.


BankTransactinApi(.Net Core Api)
ConsumerApp(node.js app) 
