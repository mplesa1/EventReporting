# EventReporting
Event Reporting in .NET Core with RabbitMq

## Setup rabbitMQ
docker run -d --hostname event-rabbit --name event-reporting-rabbit -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=user rabbitmq:3-management
