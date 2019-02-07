using System;
using SsiServerPluginContracts;
using SsiServerDto;

namespace AncoraDocPlugin
{
    public class TestWrapper : SsiPluginBase
    {
        public override string Name => "TestPlugin";
        private ISsiServerLogic _serverLogic;

        //public TestWrapper()
        //{
        //    // not required ???
        //    //first get the event from the server and implementation on the interface then proccess from ancora server create instance ??
        //    //install mvc api ? rabbit ? signalr ? endpoint ? publish/subscribe ?
          
        //}

        public override void OnServerStarted(ISsiServerLogic serverLogic)
        {
            this._serverLogic = serverLogic;
            this._serverLogic.WriteLog("Deso Was HEREEEE !!!");
        }

        public override void OnServerStopped(ISsiServerLogic serverLogic)
        {
            this._serverLogic = serverLogic; // ?? on stop dispose ??
        }
    }
}
