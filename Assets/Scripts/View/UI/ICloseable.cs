namespace View
{
    public interface ICloseable
    {
        public bool IsOpen { get; }
        public void Close();
    }
}