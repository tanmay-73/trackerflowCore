﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Util;
using RabbitMQ.Client.Events;
namespace TrackerFlow.BAL.Models
{
    public class RabbitMQBll
    {
        //public IConnection GetConnection()
        //{
        //    ConnectionFactory factory = new ConnectionFactory();
        //    factory.UserName = "admin";
        //    factory.Password = "admin";
        //    factory.Port = 5672;
        //    factory.HostName = "localhost";
        //    factory.VirtualHost = "/";
        //    //factory.Uri = "http://10.20.0.2:15672/";
        //    return factory.CreateConnection();
        //}

        //public bool send(IConnection con, string message, string friendqueue)
        //{
        //    try
        //    {
        //        IModel channel = con.CreateModel();
        //        channel.ExchangeDeclare("messageexchange", ExchangeType.Direct);
        //        channel.QueueDeclare(friendqueue, true, false, false, null);
        //        channel.QueueBind(friendqueue, "messageexchange", friendqueue, null);
        //        var msg = Encoding.UTF8.GetBytes(message);
        //        channel.BasicPublish("messageexchange", friendqueue, null, msg);

        //    }
        //    catch (Exception)
        //    {


        //    }
        //    return true;

        //}
        //public string receive(IConnection con, string myqueue)
        //{
        //    try
        //    {
        //        string queue = myqueue;
        //        IModel channel = con.CreateModel();
        //        channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        //        var consumer = new EventingBasicConsumer(channel);
        //        BasicGetResult result = channel.BasicGet(queue: queue, autoAck: true);
        //        if (result != null)
        //            return Encoding.UTF8.GetString(result.Body.ToArray());
        //        else
        //            return null;
        //    }
        //    catch (Exception)
        //    {
        //        return null;

        //    }

        //}
    }
}
