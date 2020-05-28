using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.ActionResult;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace Mumble.FirstGame.Server
{
    public class UdpServer
    {
        private Socket _socket;
        private const int _bufSize = 8 * 1024 * 2;
        private State _state = new State();
        private EndPoint _sender = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        private IActionFactory _actionFactory;
        private IScene _scene;
        private IEntityContainer _entityContainer;
        private TimeSpan _tickRate = TimeSpan.FromMilliseconds(50);
        private ConcurrentBag<IAction> _actionBuffer;

        public class State
        {
            public byte[] Buffer = new byte[_bufSize];
        }
        public UdpServer(IPEndPoint endpoint)
        {
            //TODO - move out
            Player player = new Player(3, 10);
            _entityContainer = new BattleEntityContainer(new List<Core.Entity.IMoveableCombatEntity>() { player });
            _actionFactory = new ActionFactory(_entityContainer);
            _scene = new BattleScene((BattleEntityContainer) _entityContainer, new SceneBoundary(100, 100));
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(endpoint);
        }
        public void Listen()
        {
            _actionBuffer = new ConcurrentBag<IAction>();
            SetTimer();
            _socket.BeginReceiveFrom(_state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv = (ar) =>
               {
                   State state = (State)ar.AsyncState;
                   int bytes = _socket.EndReceiveFrom(ar, ref _sender);
                   _socket.BeginReceiveFrom(state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv, state);

                   byte[] message = state.Buffer.Take(bytes).ToArray();
                   if (message[0] == message.Length)
                   {
                       IAction action = _actionFactory.Create(message.Skip(1).ToArray());
                       Console.WriteLine("RECV: {0}: {1}, {2}", _sender.ToString(), bytes, action.GetType().ToString());
                       _actionBuffer.Add(action);
                   }
                   SendNull();
               }, _state);
        }
        private void SendResults(List<IActionResult> results)
        {
            List<byte> data = new List<byte>();
            foreach (IActionResult result in results)
            {
                data.AddRange(GetResultBytes(result));      
            }
            if (data.Count > 0)
            {
                _socket.SendTo(data.ToArray(), _sender);
                Console.WriteLine("SENT: {0}: {1}", _sender.ToString(), data.Count);
            }


        }
        private List<byte> GetResultBytes(IActionResult result)
        {
            byte[] untypedData = result.ToProtobufDefinition(_entityContainer).ToByteArray();
            byte[] data = new byte[untypedData.Length + 2];
            data[0] = (byte)(untypedData.Length + 2);
            data[1] = result.GetTypeByte();
            Array.Copy(untypedData, 0, data, 2, untypedData.Length);
            return data.ToList();
        }
        private void CalculateAndReply(List<IAction> actions)
        {
            List<IActionResult> results = new List<IActionResult>();
            results.AddRange(_scene.Update(new List<IAction>(actions), 1));
            SendResults(results);
        }
        private void SendNull()
        {
            _socket.SendTo(new byte[0],_sender);
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            Timer aTimer = new System.Timers.Timer(_tickRate.TotalMilliseconds);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += TimerEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void TimerEvent(object source, ElapsedEventArgs e)
        {
            List<IAction> actions;
            lock (_actionBuffer)
            {
                actions = new List<IAction>(_actionBuffer);
                _actionBuffer = new ConcurrentBag<IAction>();
            }
            CalculateAndReply(actions);

        }
    }
}
