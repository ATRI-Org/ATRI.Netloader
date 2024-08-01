using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace stdlib.src.endstone
{
     public abstract class Permissible
    {
        protected IntPtr _ptr = IntPtr.Zero;

        [DllImport("a")]
        private unsafe static extern bool hasPermission(byte[] name, void* ptr);
        [DllImport("a")]
        private unsafe static extern bool isOp(void* ptr);

        [DllImport("a")]
        private unsafe static extern bool isPermissionSet(byte[] name, void* ptr);

        [DllImport("a")]
        private unsafe static extern void recalculatePermissions(void* ptr,void* in_ptr);

        [DllImport("a")]
        private unsafe static extern void removeAttachment(void* ptr);

        [DllImport("a")]
        private unsafe static extern void setOp(bool vaule,void* ptr);

    }
}
