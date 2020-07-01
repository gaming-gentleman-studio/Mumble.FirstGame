using Google.Protobuf;
using Mumble.FirstGame.Core.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public interface IActionResultFactory
    {
        IActionResult ToResult(byte[] data);
        IMessage ToProtobufDef(IActionResult result);
    }
}
