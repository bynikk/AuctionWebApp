using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    public static class StringExtensions
    {
        private static readonly MD5 hasher = MD5.Create();
        public static byte[] GetHash(this string str) => hasher.ComputeHash(Encoding.ASCII.GetBytes(str));
    }
}
