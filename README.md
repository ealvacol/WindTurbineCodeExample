# WindTurbine Message Provider

WindTurbine Message Provider is a lightweight and efficient messaging service designed to aggregate crucial information from wind turbines through an API and push it to a RabbitMQ queue for further processing. This project aims to streamline data collection and processing for seamless integration with other systems in the renewable energy sector.

## Project Overview

This project consists of the following key components:

1. **API Integration**: Establishes a connection with the wind turbine API to fetch real-time data, including energy production, wind speed, and turbine status.
2. **Data Aggregation**: Processes and combines the fetched data into a coherent and structured format for easy consumption by other services.
3. **RabbitMQ Queue**: Pushes the aggregated data into a RabbitMQ queue, enabling other services to consume the data efficiently and in a decoupled manner.

The WindTurbine Message Provider serves as a reliable data bridge between wind turbines and other systems, paving the way for enhanced monitoring, analytics, and decision-making in the renewable energy sector.

## Getting Started

1. Clone the repository
2. Create an instance of rabbit mq with the command `docker run -it --rm --name some-rabbit -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management`
3. Add a secrets.xml directly under the WindturbineMessageProvider Project witht the content:
	```
	<?xml version="1.0" encoding="utf-8" ?>
	<Secrets>
		<Secret key="RABBIT_MQ_PASSWORD" value="password" />
	</Secrets>
	```
4. Run the WindTurbineMessageProvider
