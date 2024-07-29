using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace netloader.commandSender
{
    /// <summary>
    /// todo : add server ptr
    /// </summary>
    public class CommandSender : Permissible
    {
        [DllImport("a")]
        private unsafe static extern void* asPlayer(void* ptr);

        [DllImport("a")]
        private unsafe static extern byte[] getName(void* ptr);

        [DllImport("a")]
        private unsafe static extern void* getServer();

        /// <summary>
        /// todo:other func
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="message"></param>
        [DllImport("a")]
        private unsafe static extern void sendErrorMessage(void* ptr, byte[] message);

        [DllImport("a")]
        private unsafe static extern void sendMessage(void* ptr, byte[] message);


    }

}
