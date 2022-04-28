using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    /// <summary>Class for hashing from string.</summary>
    public static class StringExtensions
    {
        private static readonly MD5 hasher = MD5.Create();
        /// <summary>Gets the hash.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static byte[] GetHash(this string str) => hasher.ComputeHash(Encoding.ASCII.GetBytes(str));
    }
}
