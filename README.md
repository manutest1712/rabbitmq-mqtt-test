
## Step 1: Create directory 

``` sh
mkdir rabbitmq-mqtt
```

## Step 2: Copy docker-compose.yml to the directory

## Step 3: Launch the RabbitMQ Container
``` sh
docker compose up -d --build
```
## Step 3: Enable MQTT Plugin
``` sh
docker exec -it rabbitmq rabbitmq-plugins enable rabbitmq_mqtt
```
Verify that it’s enabled:

``` sh
docker exec -it rabbitmq rabbitmq-plugins list | grep mqtt
```

You should see output like this:

``` sh
[E*] rabbitmq_mqtt
```
