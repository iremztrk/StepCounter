using SkiaSharp;

namespace StepCounter.Views
{
    internal class ChartEntry
    {
        private int v;

        public ChartEntry(int v)
        {
            this.v = v;
        }

        public string Label { get; set; }
        public string ValueLabel { get; set; }
        public SKColor Color { get; set; }
    }
}