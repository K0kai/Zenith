using ArcherTools_0._0._1.enum_things;
using InputSimulatorEx;
using InputSimulatorEx.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using InputSimulator = InputSimulatorEx.InputSimulator;

namespace ArcherTools_0._0._1.controllers
{    
    internal class KeystrokeHandler
    {

        static InputSimulator sim = new InputSimulator();
        public static void sendKeystroke(KeysEnum.SendKey keyValue, KeysEnum.SendKey modifier1 = KeysEnum.SendKey.Null, KeysEnum.SendKey modifier2 = KeysEnum.SendKey.Null)
        {
            if (modifier1 == KeysEnum.SendKey.Null)
            {
                SendKeys.Send(keyValue.StringValue());
            }
            else if (modifier1 != KeysEnum.SendKey.Null && modifier2 == KeysEnum.SendKey.Null) {
                SendKeys.Send(modifier1.StringValue() + keyValue.StringValue());
            
            }
            else
            {
                SendKeys.Send(modifier1.StringValue() + modifier2.StringValue() + keyValue.StringValue());
            }
            Thread.Sleep(100);
        }

        public static void TypeText(string stringText)
        {
            if (stringText != null)
            {
                sim.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                var _ = sim.Keyboard.TextEntry(stringText);
            }
            Thread.Sleep(100);
        }
    }
}
