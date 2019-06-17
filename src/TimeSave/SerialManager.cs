using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSave
{
    internal class SerialManager : IDisposable
    {
        /// <summary>
        /// The serial port to communicate over.
        /// </summary>
        private SerialPort _serialPort;

        /// <summary>
        /// The bytes being read from the serial port.
        /// </summary>
        private static readonly BlockingCollection<byte[]> Queue = new BlockingCollection<byte[]>();

        /// <summary>
        /// Processed bytes by the consumer.
        /// </summary>
        private readonly List<byte> _bytes = new List<byte>();

        public EventHandler<string> OnComplete;

        /// <summary>
        /// Connect to a given COM port and set up handlers.
        /// </summary>
        /// <param name="port">COM port identifier</param>
        public void Run(string port)
        {
            if (_serialPort == null)
            {
                _serialPort = new SerialPort(port, 3600, Parity.None, 8, StopBits.One);
            }
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }


            var buffer = new byte[256];
            Action startListen = null;
            var onResult = new AsyncCallback(result => OnResult(result, startListen, _serialPort, buffer));

            Task.Run(() =>
            {
                foreach (var item in Queue.GetConsumingEnumerable())
                {
                    if (item == null || item.Length == 0)
                    {
                        return;
                    }

                    if (item[item.Length - 1] == 12)
                    {
                        var message = Encoding.UTF8.GetString(_bytes.Concat(item).ToArray());

                        Console.WriteLine($@"Message Received: {Environment.NewLine}{message}{Environment.NewLine}");

                        _bytes.Clear();
                        _serialPort.Close();
                        OnComplete?.Invoke(this, message);
                    }
                    else
                    {
                        _bytes.AddRange(item);
                    }
                }
            });

            startListen = () =>
            {
                _serialPort.BaseStream.BeginRead(buffer, 0, buffer.Length, onResult, null);
            };

            startListen();

            while (true && _serialPort.IsOpen)
            {
                // handle user's console window interaction.
            }

            Queue.CompleteAdding();

            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }

        }
        private static void OnResult(IAsyncResult result, Action startListen, SerialPort port, byte[] buffer)
        {
            try
            {
                if (!port.IsOpen)
                {
                    return;
                }

                var actualLength = port.BaseStream.EndRead(result);

                var received = new byte[actualLength];

                Buffer.BlockCopy(buffer, 0, received, 0, actualLength);

                Queue.Add(received);
            }
            catch (IOException)
            {
                Console.WriteLine("I/O exception encountered. Closing.");
                port.Close();
                Queue.CompleteAdding();
                return;
            }

            startListen();
        }

        public void Dispose()
        {
            _serialPort?.Dispose();
        }
    }
}
