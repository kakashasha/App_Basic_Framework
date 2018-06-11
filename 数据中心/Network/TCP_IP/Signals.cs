using System;
using System.Collections.Generic;
using System.Text;
namespace dotNetLab
{
    namespace Network
    {
        public static class Signals
        {

            public const byte WORDS_CTS = 41;
            public const byte WORDS_STC = 42;
            public const byte WORDS_CTC = 43;
            public const byte CLIENT_ID = 44;
            public const byte FILE_BEGIN = 45;
            public const byte FILE_TRANSFER = 46;
            public const byte FILE_END = 47;
            public const byte DOWNLOAD_FILE = 48;
            public const byte GET_ALL_FILENAMES = 49;
            public const byte BUFFER_SIZE = 50;
        }


    }
}