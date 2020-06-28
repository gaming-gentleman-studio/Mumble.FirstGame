using Microsoft.Extensions.DependencyInjection;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Timers;

namespace Mumble.FirstGame.Server
{
    public class ServerManager
    {
        private UdpServer _udpChannel;
        private TcpServer _tcpChannel;
        private IScene _scene;
        private IServerSettings _settings;
        public ServerManager(IScene scene, IServerSettings settings, IActionFactory actionFactory)
        {
            _scene = scene;
            _settings = settings;
            IPEndPoint udpEndpoint = new IPEndPoint(IPAddress.Any, settings.ServerPort);
            _udpChannel = new UdpServer(udpEndpoint,_scene, actionFactory);
            IPEndPoint tcpEndpoint = new IPEndPoint(IPAddress.Any, settings.ServerPort);
            _tcpChannel = new TcpServer(tcpEndpoint, _scene,actionFactory);

        }
        public void Listen()
        {

            _udpChannel.Listen();
            _tcpChannel.Listen();
            SetTimer();
            
            
        }
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            Timer aTimer = new System.Timers.Timer(_settings.TickRate.TotalMilliseconds);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += TimerEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void TimerEvent(object source, ElapsedEventArgs e)
        {
            _udpChannel.StartUpdateTask(_settings.UpdateCyclesPerTick);
            _tcpChannel.StartUpdateTask(0);
        }
    }
}
