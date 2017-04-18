﻿namespace FFmpeg.AutoGen
{
    using System;
    using System.Runtime.InteropServices;

    public unsafe static partial class ffmpeg
    {
        /// <summary>
        /// Gets the current time in microseconds since Jan 1, 1970
        /// </summary>
        /// <returns></returns>
        [DllImport("avutil-55", EntryPoint = "av_gettime", CallingConvention = CallingConvention.Cdecl)]
        public static extern long av_gettime();

        /// <summary>
        /// Get the current time in microseconds since some unspecified starting point.
        /// On platforms that support it, the time comes from a monotonic clock
        /// This property makes this time source ideal for measuring relative time.
        /// The returned values may not be monotonic on platforms where a monotonic
        /// clock is not available.
        /// </summary>
        /// <returns></returns>
        [DllImport("avutil-55", EntryPoint = "av_gettime_relative", CallingConvention = CallingConvention.Cdecl)]
        public static extern long av_gettime_relative();


        /// <summary>
        /// Clips a signed integer value into the amin-amax range.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="amin">The amin.</param>
        /// <param name="amax">The amax.</param>
        /// <returns></returns>
        [DllImport("avutil-55", EntryPoint = "av_clip", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int av_clip(int a, int amin, int amax);

        /// <summary>
        /// Converts rational to double.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        [DllImport("avutil-55", EntryPoint = "av_q2d", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double av_q2d(AVRational r);

        public const long AV_NOPTS_VALUE = long.MinValue;
        public const int INT_MAX = int.MaxValue;

        public static int MKTAG(params byte[] buff)
        {
            //  ((a) | ((b) << 8) | ((c) << 16) | ((unsigned)(d) << 24))
            return BitConverter.ToInt32(buff, 0);
        }

        public static void memset<T>(Array arr, T value, int repeat)
        {
            var typedArray = arr as T[];
            for (var i = 0; i < repeat; i++)
                typedArray[i] = value;
        }

        public static void memset(byte* arr, byte value, int repeat)
        {
            byte* pArr = arr;

            // Copy the specified number of bytes from source to target.
            for (int i = 0; i < repeat; i++)
            {
                *pArr = value;
                pArr++;
            }
        }

        public static void memcpy(byte* target, byte* source, int length)
        {
            // Set the starting points in source and target for the copying.
            byte* ps = source;
            byte* pt = target;

            // Copy the specified number of bytes from source to target.
            for (int i = 0; i < length; i++)
            {
                *pt = *ps;
                pt++;
                ps++;
            }
        }

        public static TimeSpan ToTimeSpan(long ffmpegTimestamp)
        {
            var totalSeconds = (double)ffmpegTimestamp / ffmpeg.AV_TIME_BASE;
            return TimeSpan.FromSeconds(totalSeconds);
        }
        public static int MKTAG(byte a, char b, char c, char d)
        {
            return MKTAG(new byte[] { a, (byte)b, (byte)c, (byte)d });
        }

        public static readonly AVRational AV_TIME_BASE_Q = new AVRational { num = 1, den = AV_TIME_BASE }; // (AVRational){1, AV_TIME_BASE}


        public const int AVERROR_EOF = -32; // http://www-numi.fnal.gov/offline_software/srt_public_context/WebDocs/Errors/unix_system_errors.html
        public const int AVERROR_EAGAIN = -11;
        public const int AVERROR_ENOMEM = -12;
        public static readonly int AVERROR_OPTION_NOT_FOUND = -MKTAG(0xF8, 'O', 'P', 'T');
    }
}
