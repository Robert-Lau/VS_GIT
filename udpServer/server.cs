﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace UDP_Server
{
    class Program
    {
        static Socket server;
        static void Main(string[] args)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.117"), 4001));//绑定端口号和IP
            Console.WriteLine("服务端已经开启");
            Thread t = new Thread(ReciveMsg);//开启接收消息线程
            t.Start();
            Thread t2 = new Thread(sendMsg);//开启发送消息线程
            t2.Start();


        }
        /// <summary>
        /// 向特定ip的主机的端口发送数据报
        /// </summary>
        static void sendMsg()
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse("192.168.1.121"), 4001);
            while (true)
            {
                string msg = Console.ReadLine();
                server.SendTo(Encoding.UTF8.GetBytes(msg), point);
            }


        }
        /// <summary>
        /// 接收发送给本机ip对应端口号的数据报
        /// </summary>
        static void ReciveMsg()
        {
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
                byte[] buffer = new byte[1024];
                int length = server.ReceiveFrom(buffer, ref point);//接收数据报
                string message = Encoding.UTF8.GetString(buffer, 0, length);
                Console.WriteLine(point.ToString() + message);

            }
        }


    }
}

