using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace Avalonia.PdfViewer.Internals;

//https://github.com/AvaloniaUI/Avalonia/discussions/13610
internal static class SkiaExtensions
{
    // ReSharper disable once InconsistentNaming
    private record SKBitmapDrawOperation : ICustomDrawOperation
    {
        public Rect Bounds { get; set; }

        public SKBitmap? Bitmap { get; init; }

        public void Dispose()
        {
            //nop
        }

        public bool Equals(ICustomDrawOperation? other) => false;

        public bool HitTest(Point p) => Bounds.Contains(p);

        public void Render(ImmediateDrawingContext context)
        {
            if (
                Bitmap is null
                || context.PlatformImpl.GetFeature<ISkiaSharpApiLeaseFeature>()
                    is not { } leaseFeature
            )
                return;

            ISkiaSharpApiLease lease = leaseFeature.Lease();
            using (lease)
            {
                if (Bitmap.Handle != 0)
                {
                    lease.SkCanvas.DrawBitmap(
                        Bitmap,
                        SKRect.Create(
                            (float)Bounds.X,
                            (float)Bounds.Y,
                            (float)Bounds.Width,
                            (float)Bounds.Height
                        )
                    );
                }
            }
        }
    }

    private class AvaloniaImage : IImage, IDisposable
    {
        private readonly SKBitmap? _source;
        SKBitmapDrawOperation? _drawImageOperation;

        public AvaloniaImage(SKBitmap? source)
        {
            _source = source;
            if (source?.Info.Size is { } size)
            {
                Size = new(size.Width, size.Height);
            }
        }

        public Size Size { get; }

        public void Dispose() => _source?.Dispose();

        public void Draw(DrawingContext context, Rect sourceRect, Rect destRect)
        {
            _drawImageOperation ??= new SKBitmapDrawOperation() { Bitmap = _source };
            //changed from sourceRect to destRect
            _drawImageOperation.Bounds = destRect;
            context.Custom(_drawImageOperation);
        }
    }

    // ReSharper disable once InconsistentNaming
    public static SKBitmap? ToSKBitmap(this Stream? stream)
    {
        if (stream == null)
            return null;
        return SKBitmap.Decode(stream);
    }

    public static IImage? ToAvaloniaImage(this SKBitmap? bitmap)
    {
        if (bitmap is not null)
        {
            return new AvaloniaImage(bitmap);
        }
        return default;
    }
}
