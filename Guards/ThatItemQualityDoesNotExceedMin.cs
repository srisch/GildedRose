namespace csharp
{
    public static partial class Guard
    {
        public static void ThatItemQualityDoesNotExceedMin(Item item)
        {
            if (item.Quality <= 0)
                item.Quality = 0;
        }
    }
}