namespace bsensor
{
    public class MapHighlightRange
    {
        private string _text;
        private double _low;
        private double _high;
        private string _color;

        public MapHighlightRange(string text, double low, double high, string color)
        {
            _text = text;
            _low = low;
            _high = high;
            _color = color;
        }

        public string Text
        {
            get { return _text; }
        }

        public double Low
        {
            get { return _low; }
        }

        public double High
        {
            get { return _high; }
        }

        public string Color
        {
            get { return _color; }
        }
    }
}
