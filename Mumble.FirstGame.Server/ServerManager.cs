using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Timers;

namespace Mumble.FirstGame.Server
{
    public class ServerManager
    {
        private UdpServer _udp;
        private TcpServer _tcp;
        private IScene _scene;
        private TimeSpan _tickRate = TimeSpan.FromMilliseconds(50);
        public ServerManager()
        {
            _scene = new BattleScene();
            IPEndPoint udpEndpoint = new IPEndPoint(IPAddress.Any, 27000);
            _udp = new UdpServer(udpEndpoint,_scene);
            IPEndPoint tcpEndpoint = new IPEndPoint(IPAddress.Any, 27000);
            _tcp = new TcpServer(tcpEndpoint, _scene);
        }
        public void Listen()
        {

            _udp.Listen();
            SetTimer();
            _tcp.Listen();
            
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
            _udp.TimerEvent(1);
        }
    }
}
