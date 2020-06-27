using Microsoft.Extensions.DependencyInjection;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
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
        private TimeSpan _tickRate = TimeSpan.FromMilliseconds(50);
        public ServerManager(IScene scene, IServerSettings settings)
        {
            //BattleEntityContainer container = new BattleEntityContainer();
            //OnlineActionAdapter adapter = new OnlineActionAdapter(container);
            //_scene = new BattleScene(container,new List<IActionAdapter> { adapter });
            _scene = scene;
            IPEndPoint udpEndpoint = new IPEndPoint(IPAddress.Any, settings.ServerPort);
            _udpChannel = new UdpServer(udpEndpoint,_scene);
            IPEndPoint tcpEndpoint = new IPEndPoint(IPAddress.Any, settings.ServerPort);
            _tcpChannel = new TcpServer(tcpEndpoint, _scene);

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
            Timer aTimer = new System.Timers.Timer(_tickRate.TotalMilliseconds);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += TimerEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void TimerEvent(object source, ElapsedEventArgs e)
        {
            _udpChannel.StartUpdateTask(1);
            _tcpChannel.StartUpdateTask(1);
        }
    }
}
