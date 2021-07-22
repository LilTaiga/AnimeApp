using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.Streams;

namespace AnimeApp.Classes.Utilities
{
    public static class FileHandler
    {
        #region Write/Read - Bytes

        public static async Task WriteBytesToLocalFolder(string _filename, byte[] _content)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(_filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBytesAsync(file, _content);
        }

        public static async Task WriteBytesToLocalCacheFolder(string _filename, byte[] _content)
        {
            StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync(_filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBytesAsync(file, _content);
        }

        public static async Task<byte[]> ReadBytesFromLocalCacheFolder(string _filename)
        {
            try
            {
                var file = await ApplicationData.Current.LocalCacheFolder.GetFileAsync(_filename);
                IBuffer buffer = await FileIO.ReadBufferAsync(file);

                return buffer.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public static async Task<byte[]> ReadBytesFromLocalFolder(string _filename)
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(_filename);
                IBuffer buffer = await FileIO.ReadBufferAsync(file);

                return buffer.ToArray();
            }
            catch
            {
                return null;
            }
        }

        #endregion



        #region Write/Read - String

        public static async Task WriteStringToLocalFolder(string _filename, string _content)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(_filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, _content);
        }

        public static async Task WriteStringToLocalCacheFolder(string _filename, string _content)
        {
            StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync(_filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, _content);
        }

        public static async Task<string> ReadStringFromLocalFolder(string _filename)
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(_filename);
                return await FileIO.ReadTextAsync(file);
            }
            catch
            {
                return null;
            }
        }

        public static async Task<string> ReadStringFromLocalCacheFolder(string _filename)
        {
            try
            {
                var file = await ApplicationData.Current.LocalCacheFolder.GetFileAsync(_filename);
                return await FileIO.ReadTextAsync(file);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
