﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lessons._03
{
    /// <summary>
    /// Change implementation of Start() and Stop() method of StartableSingleton 
    /// to be thread-safe. 
    /// In case an instance has been already started and Start() is triggered, 
    /// it should write "Refused as it has been already started.".
    /// In case an instance has not been started yet and Stop() is triggered, 
    /// it should write "Refused as it is not started yet.".
    /// </summary>
    public class TaskA
    {
        public static void Run()
        {
            var startableSingleton = new StartableSingleton();
            var randomGenerator = new Random();

            Parallel.For(0, 20, _ =>
            {
                var randomNumber = randomGenerator.Next(100);
                Thread.Sleep(randomNumber);

                var start = randomNumber % 2 == 0;

                if (start)
                {
                    startableSingleton.Start();
                }
                else
                {
                    startableSingleton.Stop();

                }
            });
        }
    }

    public class StartableSingleton
    {
        private volatile bool _isStarted;
        private object stateLock = new object();

        public void Start()
        {    
            Console.WriteLine("Starting ...");
            Thread.Sleep(10);

            lock (stateLock)
            {
                if (!_isStarted)
                {
                    _isStarted = true;
                }
                else
                {
                    Console.WriteLine("Refused as it has been already started.");
                }
            }
        }

        public void Stop()
        {   
            Console.WriteLine("Stopping ...");
            Thread.Sleep(10);

            lock (stateLock)
            {
                if (_isStarted)
                {
                    _isStarted = false;
                }
                else
                {
                    Console.WriteLine("Refused as it is not started yet.");
                }
            }
        }
    }
}