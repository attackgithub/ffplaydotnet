﻿namespace Unosquare.FFplayDotNet.Rendering
{
    using Decoding;
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Provides Video Image Rendering via a WPF Writable Bitmap
    /// </summary>
    /// <seealso cref="Unosquare.FFplayDotNet.Rendering.IRenderer" />
    internal sealed class VideoRenderer : IRenderer
    {
        #region Private State

        /// <summary>
        /// The target bitmap
        /// </summary>
        private WriteableBitmap TargetBitmap;

        /// <summary>
        /// The media element
        /// </summary>
        private MediaElement MediaElement;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoRenderer"/> class.
        /// </summary>
        /// <param name="mediaElement">The media element.</param>
        public VideoRenderer(MediaElement mediaElement)
        {
            MediaElement = mediaElement;
            Application.Current.Dispatcher.Invoke(() =>
            {
                var visual = PresentationSource.FromVisual(MediaElement);
                var dpiX = 96.0 * visual?.CompositionTarget?.TransformToDevice.M11 ?? 96.0;
                var dpiY = 96.0 * visual?.CompositionTarget?.TransformToDevice.M22 ?? 96.0;

                if (MediaElement.HasVideo)
                    TargetBitmap = new WriteableBitmap(
                        MediaElement.NaturalVideoWidth, MediaElement.NaturalVideoHeight, dpiX, dpiY, PixelFormats.Bgr24, null);
                else
                    TargetBitmap = new WriteableBitmap(1, 1, dpiX, dpiY, PixelFormats.Bgr24, null);

                MediaElement.ViewBox.Source = TargetBitmap;
            });
        }

        #endregion

        #region Public API

        /// <summary>
        /// Executed when the Pause method is called on the parent MediaElement
        /// </summary>
        public void Pause()
        {
            // placeholder
        }

        /// <summary>
        /// Executed when the Play method is called on the parent MediaElement
        /// </summary>
        public void Play()
        {
            // placeholder
        }

        /// <summary>
        /// Renders the specified media block.
        /// </summary>
        /// <param name="mediaBlock">The media block.</param>
        /// <param name="clockPosition">The clock position.</param>
        /// <param name="renderIndex">Index of the render.</param>
        public void Render(MediaBlock mediaBlock, TimeSpan clockPosition, int renderIndex)
        {
            var block = mediaBlock as VideoBlock;
            if (block == null) return;

            // Skip rendering if the application is shutting down.
            if (Application.Current == null || Application.Current.Dispatcher == null 
                || Application.Current.Dispatcher.HasShutdownStarted || Application.Current.Dispatcher.HasShutdownFinished)
                return;

            var updateRect = new Int32Rect(0, 0, block.PixelWidth, block.PixelHeight);
            Application.Current.Dispatcher.Invoke(() =>
            {
                TargetBitmap.WritePixels(updateRect, block.Buffer, block.BufferLength, block.BufferStride);
            });
        }

        /// <summary>
        /// Executed when the Pause method is called on the parent MediaElement
        /// </summary>
        public void Stop()
        {
            // placeholder
        }

        /// <summary>
        /// Executed when the Close method is called on the parent MediaElement
        /// </summary>
        public void Close()
        {
            // TODO: Clear all pixels
        }

        #endregion
    }
}
